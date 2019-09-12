var enrouteDrivers = firebase.database().ref().child("enrouteDrivers");

function checkoutTaxi(taxiID) {
    $.ajax({
        url: '/TaxiPositions/GetMapPoints',
        type: 'GET',
        data: {
            taxiId: taxiID
        },
        success: function (enroute) {
            addToMap(enroute);
        },
        error: function () {
            console.log("enroute details not found in local db");
        }
    });    
}

function addToMap(enroute) {
    var driverLocation = {
        g: enroute.driverId,
        icon: enroute.taxi.TaxiModel.TaxiModelName,
        l: [enroute.rank.Lat, enroute.rank.Long]
    };
    enrouteDrivers
        .child(enroute.driverId)
        .child('currentLocation')       
        .set(driverLocation).then(function (data) {
            //$.ajax({
            //    url: '/Availables/CheckOut',
            //    type: 'POST',
            //    data: {
            //        ID: taxiID
            //    },
            //    success: function (driver) {

            //    },
            //    error: function () {
            //        console.log("Driver not found in local db");
            //    }
            //});
            console.log('Added to map');
        }).catch(function (error) {
            console.log(error);
        });
}