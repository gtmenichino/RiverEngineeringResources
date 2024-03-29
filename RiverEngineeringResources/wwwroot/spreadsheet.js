﻿let dotnetHelper = null;
let chartInstance; // Variable to store the Chart instance
let lastClickLocation = { x: 0, y: 0 };

// Export the functions to make them accessible
//window.updateChart = updateChart;
//window.resetZoomChart = resetZoomChart;
//window.setChartInstance = setChartInstance;

var arr = [
    ['0', '5', '0.035'],
    ['10', '4', '0.035'],
    ['20', '2', '0.035'],
    ['30', '4', '0.035'],
    ['40', '5', '0.035'],
    //['Jazz', 'Honda', '2019-02-12', '', true, '$ 2.000,00', '#777700'],
    //['Civic', 'Honda', '2018-07-11', '', true, '$ 4.000,01', '#007777'],
];

//arr.pushWithEvent('add', [4, 5]);
//arr.popWithEvent('remove');

export function loadChart() {

}

/*//https://github.com/jspreadsheet/ce*/
export function load() {

    var data = [
        ['0', '5', '0.035'],
        ['10', '4', '0.035'],
        ['20', '2', '0.035'],
        ['30', '4', '0.035'],
        ['40', '5', '0.035'],
        //['Jazz', 'Honda', '2019-02-12', '', true, '$ 2.000,00', '#777700'],
        //['Civic', 'Honda', '2018-07-11', '', true, '$ 4.000,01', '#007777'],
    ];


    var changed = function (instance, cell, x, y, value) {
        var cellName = jspreadsheet.getColumnNameFromId([x, y]);
        /*$('#log').append('New change on cell ' + cellName + ' to: ' + value + '');*/

        // Convert the array to a JSON string
        const jsonString = JSON.stringify(arr);

        dotnetHelper.invokeMethodAsync('GetCells', jsonString);
    }

    jspreadsheet(document.getElementById('spreadsheet'), {
        data: arr,
        rowResize:false,
        columnDrag: false,
        columnSorting: false,
        allowDeleteColumn: false,
        columns: [
            { type: 'text', title: 'Sta (ft)', width: 100 },
            { type: 'text', title: 'Elev (ft)', width: 100 },
            { type: 'text', title: 'n', width: 80 },
            //{ type: 'text', title: 'Car', width: 120 },
            //{ type: 'dropdown', title: 'Make', width: 200, source: ["Alfa Romeo", "Audi", "Bmw"] },
            //{ type: 'calendar', title: 'Available', width: 200 },
            //{ type: 'image', title: 'Photo', width: 120 },
            //{ type: 'checkbox', title: 'Stock', width: 80 },
            //{ type: 'numeric', title: 'Price', width: 100, mask: '$ #.##,00', decimal: ',' },
            //{ type: 'color', width: 100, render: 'square', }
        ],
        onchange: changed,
        ondeleterow: changed
    });



    //// add an event listener to trigger polyline draw
    //document
    //    .getElementById("fsButton")
    //    .addEventListener("click", e => drawPolyline(e));

    //// add an event listener to cancel polyline draw
    ////document
    ////    .getElementById("cancelDraw")
    ////    .addEventListener("click", e => cancelDraw(e));

    //// declare a global variable to store a reference


    return "";
}

export function initializeDotnetHelper(dotnetReference) {
    dotnetHelper = dotnetReference;
}

// Intended to be called via IJSInterop to prompt the download of a file generated by the Blazor app.
export function promptDownload(fileName, fileBytesBase64) {
    // Create a hidden <a href=""></a> where the "href" attribute contains the contents of the file in Base-64 form.
    var hyperlink = document.createElement("a");
    hyperlink.download = fileName;
    hyperlink.href = "data:application/octet-stream;base64," + fileBytesBase64;
    hyperlink.style = "display:none;";

    // Add the link tag to the end of the DOM..
    document.body.appendChild(hyperlink);

    // Simulate a click event to prompt the download.
    hyperlink.click();

    // Remove the link from the DOM.
    document.body.removeChild(hyperlink);
}

export function updateChart() {
    // Implement the logic to update the chart
    if (chartInstance) {
        // Example: Update chart properties
        chartInstance.options.title.text = 'Updated Chart Title';
        chartInstance.update(); // Update the chart
    }
}

export function resetZoomChart() {
    // Implement the logic to reset zoom on the chart
    if (chartInstance) {
        // Example: Reset zoom
        chartInstance.resetZoom(); // Reset zoom on the chart
    }
}

// Function to set the Chart instance
export function setChartInstance(chart) {
    chartInstance = chart;
}

export function getChartElementsAtEvent(chartInstance) {
    // Check if the chart instance and event are provided
    if (chartInstance && lastClickLocation) {
        // Get the elements at the last click event
        const elements = chartInstance.getElementAtEvent(lastClickLocation);

        // Return the elements
        return elements;
    }

    // Return null if chart instance or event is not provided
    return null;
}

export function handleChartClick(clientX, clientY) {
    // Update the last clicked location
    lastClickLocation = { x: clientX, y: clientY };

    // Other actions you want to perform with the clicked location
    // ...

    // No need to call C# method here; just update the location
}


export function getLastClickLocation() {
    return lastClickLocation;
}