/// <reference path="Game.js" />

var SERVER = "http://tictactoe.northeurope.cloudapp.azure.com";
var game;

$(document).ready(function() {
    $.ajaxSetup({
        cache: false
    });

    game = new Game();

    $(document).on("click", ".square", function(event) {
        // if(game.started == true) {
            var x = $(event.target).attr("x");
            var y = $(event.target).attr("y");

            if(!$(event.target).hasClass("used")) {
                game.makeMove(x, y, event.target);
            }
        // }
    });
});