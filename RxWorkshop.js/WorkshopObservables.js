var Rx = require('rx');

exports.AnswerToLife = Rx.Observable.create(function (observer) {
        observer.onNext(42);
        observer.onCompleted();
});

exports.RandomIntegers = Rx.Observable.create(function (observer) {

    for (var i = 0; i != 10; i++) {
        observer.onNext(getRandomInt(0, 100));
    }
    observer.onCompleted();
    
});


exports.ErrornousStream = Rx.Observable.create(function (observer) {

    for (var i = 0; i != 10; i++) {
        observer.onNext('Dont print this! Its not an error!');
    }
    throw 'Catch me!';
});

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}


