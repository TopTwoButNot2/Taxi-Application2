/**
     * Handles the sign up button press.
     */
$('#driverSignup').submit(function (e) {
    e.preventDefault();

    handleSignUp();

    return false;
});



function handleSignUp() {
    var driver = {};
    $("#driverSignup").serializeArray().map(function (v) {
        driver[v.name] = v.value;
        return [v.name, v.value];
    });

    var password = 'Password01';

    // [START createwithemail]
    firebase.auth().createUserWithEmailAndPassword(driver.Email, password).then(function (user) {
        var driver = {};
        $("#driverSignup").serializeArray().map(function (v) {
            driver[v.name] = v.value;
            return [v.name, v.value];
        });
        driver.driverID = user.uid;
        $.post("/Drivers/Create", driver, function (data) {
            //do whatever with the response
            if (!data.includes('error')) {
                window.location = data;
            } else {
                alert(data.body);
            }            
        });
        //Sends an email verification to the user
        //sendEmailVerification();

    }).catch(function (error) {
        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        // [START_EXCLUDE]
        if (errorCode === 'auth/weak-password') {
            alert("The password is too weak.");
        } else {
            alert(errorMessage);
        }
        console.log(error);
        // [END_EXCLUDE]
    });
    // [END createwithemail]
}

/**
 * Sends an email verification to the user.
 */
function sendEmailVerification() {
    // [START sendemailverification]
    firebase.auth().currentUser.sendEmailVerification().then(function () {
        // Email Verification sent!
        // [START_EXCLUDE]
        toastMessage('success','Email Verification Sent!');
        // [END_EXCLUDE]
    });
    // [END sendemailverification]
}

function sendPasswordReset() {
    var email = document.getElementById('email').value;
    // [START sendpasswordemail]
    firebase.auth().sendPasswordResetEmail(email).then(function () {
        // Password Reset Email Sent!
        // [START_EXCLUDE]
        //toastMessage('success','Password Reset Email Sent!');
        // [END_EXCLUDE]
    }).catch(function (error) {
        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        // [START_EXCLUDE]
        if (errorCode === 'auth/invalid-email') {
           // toastMessage('error',errorMessage);
        } else if (errorCode === 'auth/user-not-found') {
           // toastMessage('error',errorMessage);
        }
        console.log(error);
        // [END_EXCLUDE]
    });
    // [END sendpasswordemail];
}

//document.getElementById('quickstart-sign-up').addEventListener('click', handleSignUp, false);