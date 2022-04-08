'use strict';
document.addEventListener('DOMContentLoaded', function () {
   let homeButton = document.querySelector('#homeButton');
   let blogButton = document.querySelector('#blogButton');
   let currentURL = window.location.toString();
   
   // I can feel my sanity slipping into the regex abyss
   let urlRegex = /^http[s]?:\/\/.+\/[\d]{4}\/[\d]{2}\/[\d]{2}\/.+$/;
   
   if (currentURL.includes('/blog') || currentURL.match(urlRegex) !== null) {
      blogButton.classList.add('selected');
   } else {
      homeButton.classList.add('selected');
   }
   
   let spinning = false;
   let monkey = document.querySelector('#monkey');
   
   monkey.addEventListener('click', function () {
      if (!spinning) {
         spinning = true;
         monkey.classList.add('monkeySpin');
         
         setTimeout(function () {
            monkey.classList.remove('monkeySpin');
            spinning = false;
         }, 1000);
      }
   });
   
}, false);