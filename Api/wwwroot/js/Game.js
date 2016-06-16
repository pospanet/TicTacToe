var Game = (function() {
    function Game() {
        this.playerId = null;
        this.playerName = null;

        this.gameId = null;
        this.opponent = null;

        this.tick = null;
        this.started = false;
        this.length = 10;
        this.width = 10;
        this.squareWidth = 84;

        if($.cookie("playerId") != null) {
            this.playerId = $.cookie("playerId");
            this.playerName = $.cookie("playerName");

            $("#playerName").val(this.playerName);
        }

        $("#joining, #game, #gameInfo, #won, #lost, #end").hide();
    }
    Game.prototype.create = function() {
        this.playerName = $("#playerName").val();
        $.cookie("playerName", this.playerName);

        if(this.playerId == null) {
            this.playerId = this.generateId();
            $.cookie("playerId", this.playerId);
        }

        if (this.playerName.length > 0) {
            var _this = this;
            
            $("#joining").show();
            $("#info, #game, #gameInfo, #won, #lost, #end").hide();

            $.ajax({
                url: SERVER + "/api/game/join",
                method: "POST",
                data: {
                    ID: _this.playerId,
                    DisplayName: _this.playerName
                }
            }).done(function(data, textStatus, jqXHR) {
                if(data == null) {
                    _this.tick = setTimeout(_this.create(), 5000);
                } else {
                    _this.tick = null;

                    _this.gameId = data.Id;

                    if(data.Player1.ID != _this.playerId) {
                        _this.opponent = data.Player1;
                    } else {
                        _this.opponent = data.Player2;
                    }

                    _this.start();
                }
            }).fail(function(jqXHR, textStatus, errorThrown) {
                alert("Something went wrong: " + textStatus + "\n" + errorThrown);
                $("#info").show();
                $("#joining, #game, #gameInfo, #won, #lost, #end").hide();
            });
        } else {
            alert("Please enter your name first!");
        }
    }
    Game.prototype.start = function() {
        $("#info, #joining, #won, #lost, #end").hide();

        this.generateField(this.width, this.length);

        this.tick = setInterval(this.checkMove, 1000);

        $("#game, #gameInfo").show();
        this.started = true;

        $("#opponentName").html(this.opponent.DisplayName);
    }
    Game.prototype.won = function() {
        clearInterval(this.tick);
        this.tick = null;

        $("#info, #joining, #game, #gameInfo, #lost, #end").hide();
        $("#won").show();

    }
    Game.prototype.lost = function() {
        clearInterval(this.tick);
        this.tick = null;

        $("#info, #joining, #game, #gameInfo, #won, #end").hide();
        $("#lost").show();

    }
    Game.prototype.end = function() {
        clearInterval(this.tick);
        this.tick = null;

        $("#info, #joining, #game, #gameInfo, #won, #lost").hide();
        $("#end").show();

    }
    Game.prototype.generateField = function(length, width) {
        $("#game").html("");
        
        $("#game").css("width", width * this.squareWidth);

        for (var y = 0; y < length; y++) {
            for (var x = 0; x < width; x++) {
                $("#game").append('<span x="' + x + '" y="' + y + '" class="square" />');
            }
        }
    }
    Game.prototype.makeMove = function(x, y, el) {
        // send move to server
        $(el).addClass("markX used");
    }
    Game.prototype.checkMove = function() {
        // check for opponent's move
        var x = 1;
        var y = 5;
        $("[x='" + x + "'][y='" + y + "']").addClass("markO used");
    }
    Game.prototype.generateId = function() {
        var d = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            var r = (d + Math.random()*16)%16 | 0;
            d = Math.floor(d/16);
            return (c=='x' ? r : (r&0x3|0x8)).toString(16);
        });
        return uuid;
    }
    return Game;
}());