  var map = new L.Map('map');
  var outline;
  var outlineOptions;
  var useTiles = 'osm';
  var mapUrl;
  var mapAttrib;
  var adminLevel = 0;
  var isParishGroup = false;

  function labelRelation(feature, latLng) {
      var sWidth;
        var m = new L.Marker(latLng);
        if (feature.properties && feature.properties._bblabel) {
            // m.options.title = feature.properties.name;
            m.options.opacity = 0.01;
            sWidth = feature.properties._bblabel.length * 4;
            m.bindTooltip(feature.properties._bblabel, {
                noHide: true,
                direction: "center",
                offset: [0, -10],
                permanent: true,
                className: "label-" + (isParishGroup ? "parish-group" : adminLevel.toString())
            });
        } else if (feature.properties && feature.properties.name) {
            m.options.title = feature.properties.name;
        } else {
            m.icon = null;
        }
        return m;
    }

outlineOptions = {
    color: 'red',
    fillColor: '#f03',
    fillOpacity: 0.5
};

lineOptions = {
    color: 'green',
    fillColor: '#0f3',
    fillOpacity: 0.5
};

relOptions = {
    color: "Green",
    fillColor: "Magenta",
    fillOpacity: 0.3,
    pointToLayer: labelRelation
};


  var levelOptions;

  levelOptions = [
      {
          // level 0, not used
          color: 'green',
          weight: 5,
          fillColor: '#0f3',
          fillOpacity: 0.3,
          pointToLayer: labelRelation
      },
      {
          // level 1, not used
          color: 'green',
          weight: 5,
          fillColor: '#0f3',
          fillOpacity: 0.3,
          pointToLayer: labelRelation
      },
      {
          // level 2, country
          color: 'BlueViolet',
          weight: 5,
          fill: false,
          pointToLayer: labelRelation
      },
      {
          // level 3, not used
          color: 'green',
          weight: 5,
          fillColor: '#0f3',
          fillOpacity: 0.3,
          pointToLayer: labelRelation
      },
      {
          // level 4, nation
          color: 'green',
          weight: 5,
          fill: false,
          pointToLayer: labelRelation
      },
      {
          // level 5, region
          color: 'green',
          weight: 5,
          fill: false,
          pointToLayer: labelRelation
      },
      {
          // level 6, county / unitary
          color: 'Blue',
          weight: 6,
          fillColor: 'Blue',
          fillOpacity: 0.15,
          pointToLayer: labelRelation
      },
      {
          // level 7, not used
          color: 'green',
          weight: 5,
          fill: false,
          pointToLayer: labelRelation
      },
      {
          // level 8, district / borough
          color: 'Green',
          weight: 6,
          fillColor: 'Green',
          fillOpacity: 0.15,
          pointToLayer: labelRelation
      },
      {
          // level 9, not used
          color: 'green',
          weight: 5,
          fillColor: '#0f3',
          fillOpacity: 0.2,
          pointToLayer: labelRelation
      },
      {
          // level 10, parish
          color: 'Red',
          weight: 2,
          fillColor: 'Aqua',
          fillOpacity: 0.3,
          pointToLayer: labelRelation
      },
      {
          // level 11, not used
          color: 'OrangeRed',
          weight: 2,
          fillColor: 'Orange',
          fillOpacity: 0.3,
          pointToLayer: labelRelation
      },
      {
          // level 12, used for placeholders where no OSM object exists
          color: 'Gray',
          weight: 2,
          fillColor: 'Gray',
          fillOpacity: 0.1,
          pointToLayer: labelRelation
      }
  ];
var townOptions = {
    color: 'Red',
    weight: 3,
      fillColor: 'Teal',
      fillOpacity: 0.3,
      pointToLayer: labelRelation
};
  var cityOptions = {
      color: 'Red',
      weight: 3,
      fillColor: 'Orange',
      fillOpacity: 0.3,
      pointToLayer: labelRelation
  };
  var pmeetingOptions = {
      color: 'Red',
      weight: 2,
      fillColor: "#7fff00",
      fillOpacity: 0.3,
      pointToLayer: labelRelation
  };
  var groupOptions = {
      color: 'Red',
      weight: 4,
      fill: false,
      pointToLayer: labelRelation
  };



if (useTiles === 'osm') {
    mapUrl = 'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    mapAttrib = 'Map data © openstreetmap contributors';
} else if (useTiles === 'cloudmade') {
    mapUrl = 'http://{s}.tile.cloudmade.com/37aa144eae25445ea78cac122bed5493/997/256/{z}/{x}/{y}.png';
    mapAttrib = 'Map data © <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://cloudmade.com">CloudMade</a>';
} else {
    alert('unknown tile source: ' + useTiles);
}
// var OS25kURL = "http://ooc.openstreetmap.org/os1/{z}/{x}/{y}.jpg";
  var OS25kURL = "http://geo.nls.uk/mapdata2/os/25000/{z}/{x}/{y}.png";

// Possible types: SATELLITE, ROADMAP, HYBRID, TERRAIN
var googleLayer = new L.GridLayer.GoogleMutant({ type: 'roadmap' });
var gglSatLayer = new L.GridLayer.GoogleMutant({ type: 'satellite' });
var gglHybLayer = new L.GridLayer.GoogleMutant({ type: 'hybrid' });
var gglTerLayer = new L.GridLayer.GoogleMutant({ type: 'terrain' });
var bingLayerSat = new L.BingLayer("Anwt7CdaZfFM7FsXFu1vWVtkwrpUehyWcDhKaQZWiDBVp1gN8STBRfhAu6m8lMsX", { type: "Aerial" });
var bingLayerLabels = new L.BingLayer("Anwt7CdaZfFM7FsXFu1vWVtkwrpUehyWcDhKaQZWiDBVp1gN8STBRfhAu6m8lMsX", { type: "AerialWithLabels" });

  var os25kLayer = new L.TileLayer(OS25kURL, {
      minZoom: 1,
      maxZoom: 19,
      attribution: mapAttrib,
      tms: true });
var tLayer = new L.TileLayer(mapUrl, { minZoom: 1, maxZoom: 19, attribution: mapAttrib });
var overlayMaps = new L.FeatureGroup();

var baseMaps = {
    "OSM": tLayer,
    "OS25K (OSM)": os25kLayer,
//    "Google Roads": googleLayer,
//    "Google Sat": gglSatLayer,
//    "Google Hybrid": gglHybLayer,
//    "Google Terrain": gglTerLayer,
    "Bing Maps": bingLayerSat,
    "Bing with labels": bingLayerLabels
};

var overlays = {
//    "GPX Traces": overlayMaps
};

L.control.layers(baseMaps).addTo(map);

var miltonkeynes = new L.LatLng(52.0462, -0.7430); // geographical point (longitude and latitude)
var maarssen = new L.LatLng(52.1332, 5.0359); // geographical point (longitude and latitude)
//map.setView(maarssen, 13).addLayer(tLayer);
map.setView(miltonkeynes, 13).addLayer(tLayer);
map.setZoom(13);
//map.addLayer(overlayMaps);
//map.addLayer(tLayer);

  // functions called from program
  function gotoLatLon(lat,lon) {
    map.panTo(new L.LatLng(lat,lon));
  }
  function gotoBBox(minLat,minLon,maxLat,maxLon) {
    var southWest = new L.LatLng(minLat,minLon);
    var northEast = new L.LatLng(maxLat,maxLon);
    var bounds = new L.LatLngBounds(southWest, northEast);
    map.fitBounds(bounds);
  }
function gotoZoom(z) {
    map.setZoom(z);
  }
function gotoPanZoom(lat,lon,z) {
    map.setView(new L.LatLng(lat, lon), z);
}
  function drawOutline(j) { // j.points is array of {arrays of {lat,lon}} in json format
    var pts = eval('(' + j + ')');
    var i;
    var pt;
    if(outline) {
      map.removeLayer(outline);
    }
    var lpts = [];    
    for(i=0; i<pts.points.length; i++) {
       pt = new L.LatLng(pts.points[i].lat, pts.points[i].lon);
       lpts.push(pt);
    }
    outline = new L.Polygon(lpts, outlineOptions);
    map.addLayer(outline);
    alert("loaded " + pts.points.length + " points");
  }
  
  // j.segments contains array of lines to be drawn
  
function drawMultiOutline(j) {
    var arg = eval('(' + j + ')');
    var seglist = arg.segments;
    var i, iseg;
    var pt;
    if(outline) {
        map.removeLayer(outline);
    }
    var seg;
    var mplist = [];
    for(iseg=0; iseg<seglist.length; iseg++) {
        var lpts = [];
        pts = seglist[iseg];
        for(i=0; i<pts.points.length; i++) {
            pt = new L.LatLng(pts.points[i].lat, pts.points[i].lon);
            lpts.push(pt);
        }
        mplist.push(lpts);
    }
    outline = new L.MultiPolygon(mplist, outlineOptions);
    map.addLayer(outline);
}

function drawLine(j) {
    var pts = eval('(' + j + ')');
    var i;
    var pt;
    if(outline) {
      map.removeLayer(outline);
    }
    var lpts = [];    
    for(i=0; i<pts.points.length; i++) {
       pt = new L.LatLng(pts.points[i].lat, pts.points[i].lon);
       lpts.push(pt);
    }
    outline = new L.Polyline(lpts, lineOptions);
    map.addLayer(outline);
}

function drawMultiLine(j) {
    var arg = eval('(' + j + ')');
    var seglist = arg.segments;
    var i, iseg;
    var pt;
    if(outline) {
        map.removeLayer(outline);
    }
    var seg;
    var mplist = [];
    for(iseg=0; iseg<seglist.length; iseg++) {
        var lpts = [];
        var pts = seglist[iseg];
        for(i=0; i<pts.points.length; i++) {
            pt = new L.LatLng(pts.points[i].lat, pts.points[i].lon);
            lpts.push(pt);
        }
        mplist.push(lpts);
    }
    outline = new L.MultiPolyline(mplist, lineOptions);
    map.addLayer(outline);
}

var layers = new Array();

function drawJSON(j, id) {
    var arg = eval('(' + j + ')');
    for (var x in layers) {
        if (x === id) {
            map.removeLayer(layers[x]);
            break;
        }
    }
    isParishGroup = false;
    var opts;
    if (arg && arg.properties && arg.properties.admin_level) {
        adminLevel = parseInt(arg.properties.admin_level);
        // alert("admin level " + adminLevel + " style " + arg.properties.council_style);
        if (isNaN(parseInt(adminLevel))
            || adminLevel < 0
            || adminLevel >= levelOptions.length) {
            adminLevel = 0;
        }
        // adminLevel += 0; // ensure integer
        opts = levelOptions[adminLevel];
        if (adminLevel === 10) {
            if (arg.properties.council_style === "town") {
                opts = townOptions;
            } else if (arg.properties.council_style === "city" || arg.properties.council_style === "city_and_county" || arg.properties.council_style === "city_and_district") {
                opts = cityOptions;
            } else if (arg.properties.parish_type === "parish_meeting" || arg.properties.parish_type === "joint_parish_meeting") {
                opts = pmeetingOptions;
            } else if (arg.properties.parish_type === "parish_group") { // magic value
                opts = groupOptions;
                isParishGroup = true;
            }
        } else if (adminLevel === 8 || adminLevel === 6) {
            if (arg.properties.council_style === "city" || arg.properties.council_style === "city_and_county" || arg.properties.council_style === "city_and_district") {
                opts = cityOptions;
            }
        }
    } else {
        opts = relOptions;
    }
    var l = new L.GeoJSON(arg, opts);

    layers[id] = l;
    map.addLayer(l);
    // map.fitBounds(l.getBounds());
}

function zoomToLayer(id) {
    for (var x in layers) {
        if (x === id) {
            map.fitBounds(layers[x].getBounds());
            break;
        }
    }
}

function getMapBounds() {
    var b = map.getBounds();
    var bb = { minLat: b.getSouth(), maxLat: b.getNorth(), minLon: b.getWest(), maxLon: b.getEast()};
    return bb;
}

function getMapLoc() {
    var b = map.getCenter();
    var bb = { Lat: b.lat, Lon: b.lng, Zoom: map.getZoom() };
    return bb;
}

function clearLayers() {
    for (var x in layers) {
        map.removeLayer(layers[x]);
    }
}