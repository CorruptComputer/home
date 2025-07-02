/*jslint browser: true, indent: 3, bitwise: true */

// All the code below will be run once the page content finishes loading.
document.addEventListener('DOMContentLoaded', function () {
   'use strict';
   var createNewGame;

   //------------------------------------------------------------//
   // Factory to make a new game
   createNewGame = function () {
      var self, state;

      // Create a default starting state.
      state = {
         rowCounts: [1, 2, 3],
         playerMove: true,
         againstAI: false // Boolean
      };

      // The self object contains public methods.
      self = {
         // Coin counts for each row
         getRowCoinCount: function (row) {
            return state.rowCounts[row];
         },
         setRowCoinCount: function (row, count) {
            state.rowCounts[row] = count;
         },

         // Coin counts for the whole game
         getCoinCountsArray: function () {
            return state.rowCounts.slice();
         },

         // Is player 1 the current move?
         isPlayer1Move: function () {
            return state.playerMove;
         },
         changePlayer: function () {
            state.playerMove = !state.playerMove;
         },

         // Is the game against an AI?
         isAgainstAI: function () {
            return state.againstAI;
         },
         setAgainstAI: function (isAI) {
            state.againstAI = isAI;
         }
      };

      // Freeze the self object to keep it from being modified later
      return Object.freeze(self);
   };

   // Hides the view and controller from the model code above.
   (function () {
      var game, createGameBoard, addListeners, gameBoard, aiAgent;

      gameBoard = document.querySelector('#gameBoard');

      //------------------------------------------------------------//
      // Agent for the AI, takes in a copy of the board and returns the coordinates for its move.
      aiAgent = function (board) {
         var accum, row, col, boardCopy, i;

         // go through each row to see if we can find the correct move to make it a nim sum
         for (row = 0; row < board.length; row += 1) {
            boardCopy = board.slice();
            boardCopy[row] -= 1; // remove one to simulate the first coin being removed

            while (boardCopy[row] >= 0) {
               accum = 0;

               // xor all the rows for this simulation together to find the remainder
               for (i = 0; i < boardCopy.length; i += 1) {
                  accum ^= boardCopy[i];
               }

               // if its not zero keep looking
               if (accum === 0) {
                  return {
                     x: row,
                     y: boardCopy[row]
                  };
               }

               // remove one to try the next move
               boardCopy[row] -= 1;
            }
         }

         // if the AI makes it this far there is no solution, so we shoud just make a random move
         board = game.getCoinCountsArray();
         do {
            row = Math.floor(Math.random() * board.length);
         } while (board[row] === 0); // keep trying row numbers until we get to one that is NOT empty

         // select a random number less than thats row's count to use as our column to select
         col = Math.floor(Math.random() * board[row]);
         return {
            x: row,
            y: col
         };
      };

      //------------------------------------------------------------//
      // Adds listeners to each coin for 'click' events
      addListeners = function () {
         // go through every row and column and add this listener to all the coins
         Array.prototype.slice.call(gameBoard.querySelectorAll('div')).forEach(function (rowElement, row) {
            Array.prototype.slice.call(rowElement.querySelectorAll('img')).forEach(function (colElement, col) {
               colElement.addEventListener('click', function () {
                  var disableElements, coinsLeft;

                  // make sure the move selected is valid, and if its the players move
                  if (!colElement.classList.contains('disabled') && (!game.isAgainstAI() || game.isPlayer1Move())) {
                     // disableElements is a recursive function that keeps going until it hits the last coin in the row
                     disableElements = function (element) {
                        element.classList.add('disabled');

                        if (element.nextElementSibling) {
                           disableElements(element.nextElementSibling);
                        }
                     };

                     // The row is the selected row, and the col is the selected coin
                     // math is really easy here because arrays are 0-indexed, however 
                     // I am keeping the counts starting at 1, so just set the row count 
                     // to the index of the selected coin to remove that coin and all to the right.
                     game.setRowCoinCount(row, col);
                     game.changePlayer();
                     disableElements(colElement);

                     // Check for winner:
                     coinsLeft = 0;
                     // check how many coins are left in the game
                     game.getCoinCountsArray().forEach(function (count) {
                        coinsLeft += count;
                     });

                     if (!game.isAgainstAI()) {
                        // if its just player vs player all we need to do is change the text at the bottom to either A) indicate a winner or B) indicate whose move it is
                        if (coinsLeft === 0) {
                           document.querySelector('.playerMove').textContent = 'Player ' + (game.isPlayer1Move() ? '2' : '1') + ' wins!';
                        } else {
                           document.querySelector('.playerMove').textContent = 'Player ' + (game.isPlayer1Move() ? '1' : '2') + ' is moving...';
                        }
                     } else {
                        // if the game isn't over, the AI should go
                        if (coinsLeft !== 0) {
                           // IIFE to hide these vars
                           (function () {
                              var aiMove;
                              document.querySelector('.playerMove').textContent = 'AI is moving...';

                              // make the AI decide what it wants to do
                              aiMove = aiAgent(game.getCoinCountsArray());
                              game.setRowCoinCount(aiMove.x, aiMove.y);
                              aiMove = gameBoard.children[aiMove.x].children[aiMove.y];
                              aiMove.classList.add('aiSelected');

                              // wait a second for the ai to "think" before it finalizes it's move
                              setTimeout(function () {
                                 disableElements(aiMove);
                                 game.changePlayer();
                                 document.querySelector('.playerMove').textContent = 'Player is moving...';

                                 setTimeout(function () {
                                    aiMove.classList.remove('aiSelected');
                                 }, 500);

                                 // Check for winner:
                                 coinsLeft = 0;
                                 // check how many coins are left in the game
                                 game.getCoinCountsArray().forEach(function (count) {
                                    coinsLeft += count;
                                 });

                                 // if there are none left, the AI just took the last coin, therefore it wins
                                 if (coinsLeft === 0) {
                                    document.querySelector('.playerMove').textContent = 'The computer wins!';
                                 }
                              }, 2000);
                           }());
                        // if the game IS over, the player took the last coin and should be announced to be the winner.
                        } else {
                           document.querySelector('.playerMove').textContent = 'You beat the computer!';
                        }
                     }
                  }
               }, false);
            });
         });
      };

      //------------------------------------------------------------//
      // Creates a game board to play on with the selected settings
      createGameBoard = function () {
         var p;

         // before creating a new board, remove all the pieces from the old one
         while (gameBoard.hasChildNodes()) {
            gameBoard.removeChild(gameBoard.lastChild);
         }

         game.getCoinCountsArray().forEach(function (count) {
            var rowDiv, imgElement, i;
            // rows appended to the board
            rowDiv = document.createElement('div');
            rowDiv.className = 'row';
            gameBoard.appendChild(rowDiv);

            // coins appended to rows
            for (i = 0; i < count; i += 1) {
               imgElement = document.createElement('img');
               imgElement.src = '/images/games/coin.png';
               rowDiv.appendChild(imgElement);
            }
         });

         // Player text at the bottom, appended to the board.
         p = document.createElement('p');
         p.className = 'playerMove';
         if (game.isAgainstAI()) {
            p.textContent = 'Player is moving...';
         } else {
            p.textContent = 'Player 1 is moving...';
         }
         gameBoard.appendChild(p);

         addListeners();
      };

      //------------------------------------------------------------//
      // Creates a new game when clicking 'New Game!'.
      document.querySelector('#newGame').addEventListener('click', function () {
         var rowNumber;
         game = createNewGame();
         game.setAgainstAI(document.querySelector('#isAgainstAI').checked);
         // set from extrapolating the data from the other options
         for (rowNumber = 0; rowNumber < document.querySelector('#numberOfRows').value; rowNumber += 1) {
            switch (document.querySelector('#columnConfig').value) {
            case 'triangle':
               game.setRowCoinCount(rowNumber, rowNumber + 1);
               break;
            case 'square':
               game.setRowCoinCount(rowNumber, document.querySelector('#numberOfRows').value);
               break;
            case 'random':
               game.setRowCoinCount(rowNumber, Math.floor(Math.random() * document.querySelector('#numberOfRows').value) + 1);
               break;
            }
         }

         createGameBoard();
      }, false);

      // on page load use the default values for the game
      game = createNewGame();
      createGameBoard();
   }());
}, false);