'use strict';

// Redirect from prod Cloudflare Pages domain, excludes pages test deployments and local testing
if (window.location.hostname === "nickolasgupton.pages.dev"){
   window.top.location.href = 'https://nickolas.gupton.xyz';
}

const homeButton = document.querySelector('#homeButton');
const blogButton = document.querySelector('#blogButton');
const monkey = document.querySelector('#monkey');

const datePathRegex = new RegExp('/[0-9]{4}/[0-9]{2}/[0-9]{2}/');
const { pathname } = location;

document.addEventListener('DOMContentLoaded', function () {
   if (pathname.includes('/blog/') || datePathRegex.test(pathname)) {
      blogButton.classList.add('selected');
   } else {
      homeButton.classList.add('selected');
   }
   
   let spinning = false;
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
