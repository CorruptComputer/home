'use strict';

///// Globals /////
let ManhattanDistance = false;
let generatingPoints = [];
let voronoiCanvas = document.querySelector('#canvas');
let voronoiContext = voronoiCanvas && voronoiCanvas.getContext && voronoiCanvas.getContext('2d');

///// Main Functions /////
function getPointFromEvent(ev) {
    let rect = voronoiCanvas.getBoundingClientRect();
    return {
        color: 'rgb(' + Math.floor(Math.random() * 256) + ', ' + Math.floor(Math.random() * 256) + ', ' + Math.floor(Math.random() * 256) + ')',
        x: ev.clientX - rect.left,
        y: ev.clientY - rect.top
    };
}

function updateVoronoiDiagram() {
    voronoiCanvas.width = voronoiCanvas.clientWidth;
    voronoiCanvas.height = voronoiCanvas.clientHeight;

    // If there are no points, no reason to redraw
    if (generatingPoints.length === 0) {
        voronoiContext.fillStyle = 'rgb(0, 0, 0)';
        voronoiContext.fillRect(0, 0, voronoiCanvas.width, voronoiCanvas.height);
        return;
    }

    // Pick a color for each pixel based on the closest point
    for (let x = 0; x < voronoiCanvas.width; x += 1) {
        for (let y = 0; y < voronoiCanvas.height; y += 1) {
            let currentDistance = 0;
            let closestDistance = Infinity;

            generatingPoints.forEach(function (currentPoint) {
                if (ManhattanDistance) {
                    currentDistance = (Math.abs(x - currentPoint.x) + Math.abs(y - currentPoint.y));
                } else {
                    currentDistance = Math.sqrt(Math.pow(x - currentPoint.x, 2) + Math.pow(y - currentPoint.y, 2));
                }
                
                if (currentDistance < closestDistance) {
                    closestDistance = currentDistance;
                    voronoiContext.fillStyle = currentPoint.color;
                }
            });
            voronoiContext.fillRect(x, y, 1, 1);
        }
    }

    // Draw the circle around the point
    generatingPoints.forEach(function (currentPoint) {
        // Black circle
        voronoiContext.beginPath();
        voronoiContext.arc(currentPoint.x, currentPoint.y, 7, 0, 2 * Math.PI, false);
        voronoiContext.fillStyle = 'black';
        voronoiContext.fill();

        // White circle
        voronoiContext.beginPath();
        voronoiContext.arc(currentPoint.x, currentPoint.y, 5, 0, 2 * Math.PI, false);
        voronoiContext.fillStyle = 'white';
        voronoiContext.fill();

        // Colored circle
        voronoiContext.beginPath();
        voronoiContext.arc(currentPoint.x, currentPoint.y, 3, 0, 2 * Math.PI, false);
        voronoiContext.fillStyle = currentPoint.color;
        voronoiContext.fill();
    });
}

function addPoint(ev) {
    generatingPoints.push(getPointFromEvent(ev));
    updateVoronoiDiagram();
}

function swapDistanceCalculation() {
    ManhattanDistance = !ManhattanDistance;
    if (ManhattanDistance) {
        document.body.style.backgroundColor = 'rgb(66, 7, 122)';
    } else {
        document.body.style.backgroundColor = 'rgb(22, 74, 119)';
    }

    updateVoronoiDiagram();
}

///// Event listeners /////
voronoiCanvas.addEventListener('click', (ev) => addPoint(ev), false);
document.querySelector('#title').addEventListener('click', swapDistanceCalculation, false);
window.addEventListener('resize', updateVoronoiDiagram, false);

///// Run! /////
// Finally draw for the first time
updateVoronoiDiagram();
