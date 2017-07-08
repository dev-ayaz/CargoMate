
var config = {
    apiKey: "AIzaSyAAgsBhSzZ3WgVmEbTOMJG4dDpZRKZS12M",
    authDomain: "prologix-5cc3c.firebaseapp.com",
    databaseURL: "https://prologix-5cc3c.firebaseio.com",
    storageBucket: "prologix-5cc3c.appspot.com",
    messagingSenderId: "729428235723"
};
firebase.initializeApp(config);




var firebaseUtilFunc = {
    logout: function () {
        window.firebase.auth().signOut().then(function (user) {
            console.log(user);
        }, function (error) {
            console.log(error);
        });
    },
    signInWithEmailAndPassword: function (email, password) {
        window.firebase.auth().signInWithEmailAndPassword(email, password).catch(function (error) {

            console.log(error);
            // Handle Errors here.
            var errorCode = error.code;
            var errorMessage = error.message;
            // ...
        });
    },
    createUserWithEmailAndPassword: function () {
        window.firebase.auth().createUserWithEmailAndPassword(email, password).catch(function (error) {
            // Handle Errors here.
            console.log(error);
            var errorCode = error.code;
            var errorMessage = error.message;
            // ...
        });
    }
}