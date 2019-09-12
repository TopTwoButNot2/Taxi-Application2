var enrouteDrivers = firebase.database().ref().child("enrouteDrivers");

var map = new google.maps.Map(document.getElementById('map'), {
    zoom: 10,
    center: new google.maps.LatLng(-29.8579, 31.0292),
    mapTypeId: google.maps.MapTypeId.ROADMAP
});
var infowindow = new google.maps.InfoWindow();
var marker, i;
var mapMarker = [];

enrouteDrivers.on("child_added", snapshot => {
    addMarker(snapshot);
});
enrouteDrivers.on("child_removed", function (snapshot) {
    removeMarker(snapshot);
});
enrouteDrivers.on("child_changed", function (snapshot) {
    moveMarker(snapshot);
});

function addMarker(snapshot) {
    $.ajax({
        url: '/Drivers/getDriver',
        type: 'GET',
        data: {
            driverId: snapshot.key
        },
        success: function (driver) {
            marker = new google.maps.Marker({
                position: new google.maps.LatLng(snapshot.child("currentLocation").val().l[0], snapshot.child("currentLocation").val().l[1]),
                icon: '../Images/icons/' + snapshot.child("currentLocation").val().icon + '.jpg',
                map: map
            });
            
            var infoCard = '<table>' +
                '<tr>' +
                '<td>' +
                '<img width="60" height="60" alt="" src="../Images/icons/' + snapshot.child("currentLocation").val().icon + '.jpg" style="border-radius: 50%;  margin: 0 auto;" />' +
                '</td>' +
                '<td>' +
                '<table >' +
                '<tr>' +
                '<td style="min-width:20px"><span><i class="fa fa-user"></i></span> <span class="pull-right">:</span> </td>' +
                '<td id="user"> <a href="/Users/Details?uid=' + snapshot.key + '" rel= "tooltip" title="Click to view profile">' + driver.FirstName + ' ' + driver.LastName + '</a> </td>' +
                '</tr>' +
                '<tr>' +
                '<td><span><i class="fa fa-at"></i></span> <span class="pull-right">:</span> </td>' +
                '<td id="user_email"><a href="mailto:' + driver.Email + '">' + driver.Email + '</a></td>' +
                '</tr>' +
                '<tr>' +
                '<td><span><i class="fa fa-phone"></i></span> <span class="pull-right">:</span></td>' +
                '<td id="user_phone"><a href="tel:' + driver.PhoneNumber + '">' + driver.PhoneNumber + '</a></td>' +
                '</tr>' +
                '</table>' +
                '</td>' +
                '</tr >' +
                '<tr>' +
                '<td>' +
                '<img width="60" height="60" alt="" src="../Images/icons/' + snapshot.child("currentLocation").val().icon + '.jpg" style="border-radius: 50%;  margin: 0 auto;" />' +
                '</td>' +
                '<td>' +
                '<table >' +
                '<tr>' +
                '<td style="min-width:20px"><span><i class="fa fa-car"></i></span> <span class="pull-right">:</span> </td>' +
                '<td >' + snapshot.child("currentLocation").val().icon + '</td>' +
                '</tr>' +
                //'<tr>' +
                //'<td><span><i class="fa fa-registered"></i></span> <span class="pull-right">:</span> </td>' +
                //'<td >' + user.data.val().car_registration_number + '</td>' +
                //'</tr>' +
                //'<tr>' +
                //'<td><span><i class="fa fa-palette"></i></span> <span class="pull-right">:</span></td>' +
                //'<td>' + user.data.val().vehicle_color + '</td>' +
                //'</tr>' +
                '</table>' +
                '</td>' +
                '</tr>' +
                '<tr>' +
                '<td colspan="2" class="text-center">' +
                '<button  onclick = "showWriteToUserDialog(\'' + snapshot.key + '\',\'Drivers\')" class="btn btn-default"><i class="fa fa-edit"></i> Write Message</button>' +
                '</td> ' +
                '</tr>' +
                '</table>' +
                '</td>' +
                '</tr>' +
                '</table>';

            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                    infowindow.setContent(infoCard);
                    infowindow.open(map, marker);
                };
            })(marker, i));

            mapMarker.push({ marker: marker, key: snapshot.child("currentLocation").val().g });
        },
        error: function () {
            console.log("Driver not found in local db");
        }
    });
}
function removeMarker(snapshot) {
    for (i = 0; i < mapMarker.length; i++) {
        if (mapMarker[i].key.includes(snapshot.child("currentLocation").val().g)) {
            mapMarker[i].marker.setMap(null);
        }
    }
}
function moveMarker(snapshot) {
    for (i = 0; i < mapMarker.length; i++) {
        if (mapMarker[i].key.includes(snapshot.child("currentLocation").val().g)) {
            mapMarker[i].marker.setPosition(new google.maps.LatLng(snapshot.child("currentLocation").val().l[0], snapshot.child("currentLocation").val().l[1]));
        }
    }
}
function clearMapMarker() {
    for (i = 0; i < mapMarker.length; i++) {
        mapMarker[i].setMap(null);
    }
    mapMarker = [];
}

