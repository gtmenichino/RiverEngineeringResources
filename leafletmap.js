
//import 'https://unpkg.com/leaflet@1.7.1/dist/leaflet.js';

let map;
//let drawnLayer;
let drawnFeatures;
let dotnetHelper = null;

let drawnFloodingSources;

var openLayer;


//window.initializeMap = (mapId) => {
//    const map = L.map(mapId).setView([51.505, -0.09], 13);

//    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
//        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
//    }).addTo(map);

//    map.pm.addControls({
//        position: 'topleft',
//        drawCircle: false,
//    });
//};

export function load_map2() {


    const map = L.map('map').setView([38.649470, -121.600108], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    var options = {
        position: 'topleft',
        drawMarker: false,
        drawPolygon: false,
        drawPolyline: false,
        drawCircle: false,
        drawRectangle: false,
        drawText: false,
        drawCircleMarker: false,
        editPolygon: true,
        removalMode: true,
        editMode: true,
        editControls: true,
        rotateMode: false,
        cutPolygon: false,
        dragMode: false
    };

    map.pm.addControls(options);

    drawnFloodingSources = new L.FeatureGroup();
    map.addLayer(drawnFloodingSources);

    var decorator;

    //// creates new actions
    //const actions1 = [
    //    'finishMode',
    //    // creates a new action with text and a click event
    //    {
    //        text: "Edit",
    //        onClick: () => {
    //            map.pm.Draw.FloodingSource._layer.pm.enable({
    //                allowSelfIntersection: false,
    //            });
    //            //alert("🙋‍♂️");
    //        },
    //    },
    //    // uses the default 'cancel' action
    //    "cancel",
    //];
    const handleEdit = (e) => {
        console.log("Element was edited", e);
    };

    map.pm.Toolbar.copyDrawControl('Line', {
        name: 'FloodingSource',
        block: 'draw',
        title: 'Flooding Source',
        actions: ['finishMode',
            //// creates a new action with text and a click event
            //{
            //    text: "Edit",
            //    onClick: () => {
            //        if (drawnFloodingSources.pm.enabled() == false) {
            //            drawnFloodingSources.pm.enable();
            //        } else {
            //            drawnFloodingSources.pm.disable();
            //        }
            //        //map.pm.Draw.FloodingSource.disableDraw();


            //        //map.pm.disableDraw();

            //        //map._layer["FloodingSource"].pm.enable();
            //        //map.pm.Draw.FloodingSource._layer.pm.enable({
            //        //    allowSelfIntersection: false,
            //        //});
            //        //alert("🙋‍♂️");
            //    },
            //},
            //// uses the default 'cancel' action
            "cancel",]
    });

    map.pm.Draw.FloodingSource.setOptions({
        pathOptions: { color: 'blue' },
        hintlineStyle: { color: 'blue', dashArray: [5, 5] },
        templineStyle: { color: 'blue' }
    });


    map.pm.Toolbar.copyDrawControl('Polygon', {
        name: 'FlowArea',
        block: 'draw',
        title: 'Flow Area',
        actions: ['finishMode',
            "cancel",]
    });

    map.pm.Draw.FlowArea.setOptions({
        pathOptions: { color: 'black' },
        hintlineStyle: { color: 'black', dashArray: [5, 5] },
        templineStyle: { color: 'black' }
    });



    map.pm.Toolbar.copyDrawControl('Line', {
        name: 'Dam',
        block: 'draw',
        title: 'Dam',
        actions: ['finishMode',
            "cancel",]
    });

    map.pm.Draw.Dam.setOptions({
        pathOptions: { color: 'red' },
        hintlineStyle: { color: 'red', dashArray: [5, 5] },
        templineStyle: { color: 'red' }
    });




    const shapes = [{
        "type": "Feature",
        "properties": {},
        "geometry": {
            "type": "Polygon",
            "coordinates": [
                [
                    [-3.701856, 40.422481],
                    [-3.707092, 40.418593],
                    [-3.70177, 40.417809],
                    [-3.701899, 40.422873],
                    [-3.701856, 40.422481]
                ]
            ]
        }
    }];
    const geojson = L.geoJSON(shapes).addTo(map);




    // listen to when drawing mode gets enabled
    map.on('pm:drawstart', function (e) {
        console.log(e)

        if (e.shape === 'FloodingSource') {
            //map.pm.Draw.FloodingSource._hintMarker.on('move', (e) => {
            //    var latlngs = map.pm.Draw.FloodingSource._layer.getLatLngs();
            //    latlngs = latlngs.concat(e.latlng);
            //    if (latlngs.length > 1) {
            //        if (decorator) {
            //            decorator.removeFrom(map);
            //        }
            //        decorator = updateDecorator(latlngs);
            //    }
            //})
        }
    });

    // listen to when a new layer is created
    map.on('pm:create', (e) => {
        var type = e.layerType;
        var layer = e.layer;
        let feature = (layer.feature = layer.feature || {});

        feature.type = feature.type || "Feature";
        let props = (feature.properties = feature.properties || {});
        props.type = type;

        console.log(e)

        if (e.shape === 'FloodingSource') {
            props.mycustomprop = "hi there"
            props.name = "myname"
            props.description = "mydescription"
            props.type = "FloodingSource"
            

            var idIW = L.popup();
            //var content = '<span><b>Shape Name</b></span><br/><input id="shapeName" type="text"/><br/><br/><span><b>Shape Description<b/></span><br/><textarea id="shapeDesc" cols="25" rows="5"></textarea><br/><br/><input type="button" id="okBtn" value="Save" onclick="saveIdIW()"/>';

            var content = '<span><b>Shape Name</b></span><br/><input id="shapeName" type="text"/><br/><br/><span><b>Shape Description<b/></span><br/><textarea id="shapeDesc" cols="25" rows="5"></textarea><br/><br/"/>';

            idIW.setContent(content);
            var latlng1 = e.layer._latlngs;
            idIW.setLatLng(latlng1[0]); //calculated based on the e.layertype
            //idIW.openOn(map);
            const div = document.createElement("div");
            div.innerHTML = `<br>${props.name}<br>`;
            const button = document.createElement("button");
            button.innerHTML = "Save";
            button.onclick = function () {
                saveIdIW();
            }
            div.appendChild(button);

            //><input type="button" id="okBtn" value="Save" onclick="saveIdIW()

            e.layer.bindPopup(div);//'Flooding Source').openPopup();



            //decorator.removeFrom(map);
            //updateDecorator(e.layer)
            //drawnFloodingSources.addLayer(e.layer);
            //decorator.removeFrom(map);
            //updateDecorator(drawnFloodingSources);


            // Convert the drawn layer to GeoJSON and send it to C#
            const geoJson = e.layer.toGeoJSON();
            var geojsonStr = JSON.stringify(geoJson);
            dotnetHelper.invokeMethodAsync('AddDrawnFeature', JSON.stringify(geoJson));
            



            // listen to changes on the new layer
            e.layer.on('pm:edit', function (x) {
                console.log('edit', x)
                //drawnFloodingSources.deleteArrowheads();
                //decorator.removeFrom(map);
                //updateDecorator(drawnFloodingSources);
                //drawnFloodingSources.addLayer(x.layer);

                //var latlngs = map.pm.Draw.FloodingSource._layer.getLatLngs();
                ////latlngs = latlngs.concat(e.latlng);
                //if (latlngs.length > 1) {
                //    if (decorator) {
                //        decorator.removeFrom(map);
                //    }
                //    decorator = updateDecorator(latlngs);
                //}

            });

            // listen to changes on the new layer
            e.layer.on('pm:remove', function (x) {
                console.log('remove', x)
                //decorator.removeFrom(map);
                //updateDecorator(drawnFloodingSources);
            });

            // listen to changes on the new layer
            e.layer.on('pm:enable', function (x) {
                console.log('enable', x)
                //map.pm.Draw.FloodingSource._layer.deleteArrowheads();
                //map.pm.Draw.FloodingSource._layer = true;
                //decorator.removeFrom(map);
                //updateDecorator(x.layer)
                //drawnFloodingSources.addLayer(x.layer);
            });

            // listen to changes on the new layer
            e.layer.on('pm:disable', function (x) {
                console.log('disable', x)

                //var latlngs = map.pm.Draw.FloodingSource._layer.getLatLngs();
                ////latlngs = latlngs.concat(e.latlng);
                //if (latlngs.length > 1) {
                //    if (decorator) {
                //        decorator.removeFrom(map);
                //    }
                //    decorator = updateDecorator(latlngs);
                //}

                //decorator.removeFrom(map);
                //updateDecorator(drawnFloodingSources);
            });
        }

        if (e.shape === 'FlowArea') {
            props.mycustomprop = "hi there"
            props.name = "myname"
            props.description = "mydescription"
            props.type = "FlowArea"

            var idIW = L.popup();
            var content = '<span><b>Shape Name</b></span><br/><input id="shapeName" type="text"/><br/><br/><span><b>Shape Description<b/></span><br/><textarea id="shapeDesc" cols="25" rows="5"></textarea><br/><br/"/>';
            idIW.setContent(content);
            var latlng1 = e.layer._latlngs;
            idIW.setLatLng(latlng1[0]); //calculated based on the e.layertype
            //idIW.openOn(map);
            const div = document.createElement("div");
            div.innerHTML = `<br>${props.name}<br>`;
            const button = document.createElement("button");
            button.innerHTML = "Save";
            button.onclick = function () {
                saveIdIW();
            }
            div.appendChild(button);
            e.layer.bindPopup(div);//'Flooding Source').openPopup();

            // Convert the drawn layer to GeoJSON and send it to C#
            const geoJson = e.layer.toGeoJSON();
            var geojsonStr = JSON.stringify(geoJson);
            dotnetHelper.invokeMethodAsync('AddDrawnFeature', JSON.stringify(geoJson));

            // listen to changes on the new layer
            e.layer.on('pm:edit', function (x) {
                console.log('edit', x)
                //drawnFloodingSources.deleteArrowheads();
                //decorator.removeFrom(map);
                //updateDecorator(drawnFloodingSources);
                //drawnFloodingSources.addLayer(x.layer);

                //var latlngs = map.pm.Draw.FloodingSource._layer.getLatLngs();
                ////latlngs = latlngs.concat(e.latlng);
                //if (latlngs.length > 1) {
                //    if (decorator) {
                //        decorator.removeFrom(map);
                //    }
                //    decorator = updateDecorator(latlngs);
                //}

            });

            // listen to changes on the new layer
            e.layer.on('pm:remove', function (x) {
                console.log('remove', x)
                //decorator.removeFrom(map);
                //updateDecorator(drawnFloodingSources);
            });

            // listen to changes on the new layer
            e.layer.on('pm:enable', function (x) {
                console.log('enable', x)
                //map.pm.Draw.FloodingSource._layer.deleteArrowheads();
                //map.pm.Draw.FloodingSource._layer = true;
                //decorator.removeFrom(map);
                //updateDecorator(x.layer)
                //drawnFloodingSources.addLayer(x.layer);
            });

            // listen to changes on the new layer
            e.layer.on('pm:disable', function (x) {
                console.log('disable', x)

                //var latlngs = map.pm.Draw.FloodingSource._layer.getLatLngs();
                ////latlngs = latlngs.concat(e.latlng);
                //if (latlngs.length > 1) {
                //    if (decorator) {
                //        decorator.removeFrom(map);
                //    }
                //    decorator = updateDecorator(latlngs);
                //}

                //decorator.removeFrom(map);
                //updateDecorator(drawnFloodingSources);
            });
        }



    });

    function saveIdIW() {
        var sName = $('#shapeName').val();
        var sDesc = $('#shapeDesc').val();

        var drawings = drawnFloodingSources.getLayers();  //drawnItems is a container for the drawn objects
        drawings[drawings.length - 1].title = sName;
        drawings[drawings.length - 1].content = sDesc;

        if (idIW) {
            map.closePopup();
        }
    }

    // listen to when drawing mode gets disabled
    map.on('pm:drawend', function (e) {
        console.log(e)
    });

    //remove the arrows from the lines at the start of the edit and then add arrows when edit is finished.
    map.on('pm:globaleditmodetoggled', (evt) => {

        //L.PM.Utils.findLayers(map).forEach((layer) => {
        //    if (layer._arrowheads || layer._hasArrowheads) {
        //        if (evt.enabled) {
        //            layer.deleteArrowheads();
        //            layer._hasArrowheads = true;
        //        }
        //        else {
        //            layer.remove();
        //            layer.arrowheads(arrowheadsOptions);
        //            layer.addTo(map);
        //        }
        //    }
        //})
    });


    function updateDecorator(layer) {

        return L.polylineDecorator(layer, {
            patterns: [
                // defines a pattern of 10px-wide dashes, repeated every 20px on the line
                { offset: 0, repeat: 50, symbol: L.Symbol.arrowHead({ pixelSize: 8, polygon: false, pathOptions: { color: 'green', stroke: true } }) }
            ]
        }).addTo(map);
    }

    //    To add a layers to a FeatureGroup you can use map.pm.setGlobalOptions({ layerGroup: YOUR_GROUP });

    //And to get all drawn layers you can call map.pm.getGeomanDrawLayers(true).toGeoJSON() or because you have your own group: YOUR_GROUP.toGeoJSON()



    map.on('pm:remove', (e) => {
        const shapes = [];

        //if (e.shape === 'FloodingSource') {
        //    //e.layer.bindPopup('Flooding Source').openPopup();
        //    //decorator.removeFrom(map);
        //    //updateDecorator(e.layer)

        //}

        map.eachLayer((layer) => {
            if (layer.pm) {
                const geojson = layer.toGeoJSON();

                if (layer instanceof L.Circle) {
                    geojson.properties.radius = 10;
                }

                shapes.push(geojson);
            }
        });



        console.log('pm:remove');
        console.log(shapes);
    });

    return "";
};

export function load_map() {
    map = L.map('map').setView([38.649470, -121.600108], 13);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 19 }).addTo(map);

    // Initialize a GeoJSON layer for drawn features
    //drawnLayer = L.geoJSON().addTo(map);
    drawnFeatures = new L.FeatureGroup();
    map.addLayer(drawnFeatures);

    //initialize leaflet draw control and add it to the map
    var drawControl = new L.Control.Draw({
        //draw: {
        //    polyline: false, // Enable drawing polylines
        //    polygon: false,
        //    rectangle: false,
        //    circle: false,
        //    marker: false,
        //},
        edit: {
            featureGroup: drawnFeatures
        }
    });
    map.addControl(drawControl);


    // add an event listener to trigger polyline draw
    document
        .getElementById("fsButton")
        .addEventListener("click", e => drawPolyline(e));

    // add an event listener to cancel polyline draw
    //document
    //    .getElementById("cancelDraw")
    //    .addEventListener("click", e => cancelDraw(e));

    // declare a global variable to store a reference
    let polylineDrawHandler;

    // store the polyline draw instantiation to a variable to be able 
    // to disable it later
    const drawPolyline = e => {
        polylineDrawHandler = new L.Draw.Polyline(map, drawControl.options.polyline);
        polylineDrawHandler.enable();
    };

    //const cancelDraw = e => polylineDrawHandler.disable();






    map.on("draw:created", function (e) {
        var type = e.layerType;
        var layer = e.layer;
        let feature = (layer.feature = layer.feature || {});

        feature.type = feature.type || "Feature";
        let props = (feature.properties = feature.properties || {});

        props.type = type;

        if (type === "circle") {
            props.radius = layer.getRadius();
        }

        drawnFeatures.addLayer(layer);

        // Convert the drawn layer to GeoJSON and send it to C#
        //const geoJson = layer.toGeoJSON();

        //dotnetHelper.invokeMethodAsync('AddDrawnFeature', JSON.stringify(geoJson));

        //var geojsonStr = JSON.stringify(geoJson);
    });


    return "";
}

export function initializeDotnetHelper(dotnetReference) {
    dotnetHelper = dotnetReference;
}


//map.on('draw:created', function (e) {
//     var idIW = L.popup();
//     var content = '<span><b>Shape Name</b></span><br/><input id="shapeName" type="text"/><br/><br/><span><b>Shape Description<b/></span><br/><textarea id="shapeDesc" cols="25" rows="5"></textarea><br/><br/><input type="button" id="okBtn" value="Save" onclick="saveIdIW()"/>';
//     idIW.setContent(content);
//     idIW.setLatLng(latlng); //calculated based on the e.layertype
//     idIW.openOn(map);
//}

//function saveIdIW() {
//    var sName = $('#shapeName').val();
//    var sDesc = $('#shapeDesc').val();

//    var drawings = drawnItems.getLayers();  //drawnItems is a container for the drawn objects
//    drawings[drawings.length - 1].title = sName;
//    drawings[drawings.length - 1].content = sDesc;

//    if (idIW) {
//        map.closePopup();
//    }
//}


//export function enableDrawingMode() {
//    // Enable drawing mode using Leaflet.Draw
//    const drawnControl = new L.Control.Draw({
//        draw: {
//            polyline: true, // Enable drawing polylines
//            polygon: false,
//            rectangle: false,
//            circle: false,
//            marker: false,
//        },
//        edit: {
//            featureGroup: drawnLayer, // Set the feature group for editing
//        },
//    });
//    map.addControl(drawnControl);
//}

//function initializeDrawing() {
//    map.on(L.Draw.Event.CREATED, function (e) {
//        const layer = e.layer;

//        // Add the drawn layer to the map
//        drawnLayer.addLayer(layer);

//        // Convert the drawn layer to GeoJSON and send it to C#
//        const geoJson = layer.toGeoJSON();
//        dotnetHelper.invokeMethodAsync('AddDrawnFeature', JSON.stringify(geoJson));
//    });
//}