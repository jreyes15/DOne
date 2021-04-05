app.controller('trainController', function ($scope, $http, $location, $window) {
    var vm = $scope;
    var canvas = document.getElementById('canvas');
    var context = canvas.getContext('2d');
    vm.result = [];
    // Function to do all calcs
    vm.doWork = function () {
        // to process the input text
        getResult(vm.currentScript);
    }
    // Function to draw the area and the grid bounds (red)
    function drawBase(gridBounds) {
        // Box width
        var bw = 400;
        // Box height
        var bh = 400;
        // Padding
        var p = 40;
        // Loop to draw the grid lines X
        for (var x = 0; x <= bw; x += 40) {
            context.moveTo(0.5 + x + p, p);
            context.lineTo(0.5 + x + p, bh + p);
        }
        // Loop to draw the grid lines Y
        for (var x = 0; x <= bh; x += 40) {
            context.moveTo(p, 0.5 + x + p);
            context.lineTo(bw + p, 0.5 + x + p);
        }
        // set the line color for the grid
        context.strokeStyle = "gray";
        context.stroke();
        // Calc the grid bounds
        var gridBoundX = p * gridBounds.X;
        var gridBoundY = p * gridBounds.Y;
        // set the color red to the grid bound lines
        context.strokeStyle = "#FF0000";
        // Draw the grid bound (with a rectangle)
        context.strokeRect(p, bh + p, gridBoundX, -gridBoundY);
        context.closePath();
    }
    // Function to draw the breadcrumbs rover
    function drawBreadcumb(rover) {
        //Declarations
        // Padding 40px
        var p = 40;
        // Position X = 0, to alocate on the buttom left corner
        var startingX = 0;
        // Position Y = 0, to alocate on the buttom left corner
        var startingY = 400;
        var toX = 0;
        var toY = 0;
        // constant to get random color to draw breadcumbs lines
        const randomColor = Math.floor(Math.random() * 16777215).toString(16);
        //starting the breadcumbs lines path
        context.beginPath();
        for (j = 0; j < rover.movements.length; j++) {
            // starting position X & Y
            // the variable p is the width & heigth of each cell
            startingX = (rover.movements[j].lastValueXAxis * p);
            startingY = (400 - (rover.movements[j].lastValueYAxis * p));
            // lines to X & Y
            // It is necessary to add the padding value to the X coordinate
            toX = p + (rover.movements[j].currentValueXAxis * p);
            //It is necessary to add the value of the padding to the Y coordinate, before subtracting the value from the height of the canvas
            toY = (400 - (rover.movements[j].currentValueYAxis * p) + p);
            // move to the starting position (X & Y)
            context.moveTo(startingX + p, startingY + p);
            // If it's the first position draw an rectangle with green color to indicates the starting point
            if (j == 0) {
                context.strokeStyle = "green";
                context.strokeRect(startingX + p - 2, startingY + p - 2, 4, 4);
            }
            // Drawing the line
            context.lineTo(toX, toY);
            // Set the line weight
            context.lineWidth = 1;
            // Set the color by randonColor variable
            context.strokeStyle = "#" + randomColor;
            context.stroke();
            // If it's the last position draw an rectangle with black color to indicates the end point, to center the element, half the width and height are subtracted
            if (j == rover.movements.length - 1) {
                context.fillRect(toX - 3, toY - 3, 6, 6);
            }
        }
        context.closePath();
    }
    // Funtion to send the text to the C# Controller in order to process it.
    function getResult(text) {
        $http({
            url: "Home/doWork",
            method: "POST",
            params: { value: text }
        }).then(function (data) {
            vm.result = data.data;
            // to draw the grid bounds
            drawBase(vm.result.gridBounds);
            // Send each breadcrumb rover to draw
            for (i = 0; i < vm.result.rovers.length; i++) {
                drawBreadcumb(vm.result.rovers[i]);
            }
        });
    }
});