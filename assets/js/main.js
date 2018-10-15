document.addEventListener('DOMContentLoaded', function () {
   'use strict';
   var spinning = false;
   
   document.querySelector('#monkey').addEventListener('click', function () {
      if (!spinning) {
         spinning = true;
         document.querySelector('#monkey').classList.add('monkeySpin');
         
         setTimeout(function () {
            document.querySelector('#monkey').classList.remove('monkeySpin');
            spinning = false;
         }, 1000);
      }
   });
   
}, false);