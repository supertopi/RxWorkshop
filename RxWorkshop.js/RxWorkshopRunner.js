//Keep the process alive
process.stdin.setRawMode(true);
process.stdin.resume();
process.stdin.on('data', process.exit.bind(process, 0));

var Rx = require('rx');
var WorkshopObservables = require('./WorkshopObservables.js')

Rx.Observable.return('Welcome to RxWorkshop.JS ! Remember to check http://reactivex.io/')
    .subscribe(function (i) { console.log(i) });

//ASSIGNMENTS

// 1. Observe WorkshopObservables.AnswerToLife and print out the answer

// 2. Print out the current datetime every second 3 times
//    TIPS: interval, map, take

//3 . Observe WorkshopObservables.RandomIntegers. Take 10 values, print each value and finally print their sum
//    TIPS: do, sum

//4. Observe WorkshopObservables.ErrornousStream. When an error occurs, print it out and terminate the observable sequence.
     //TIPS: catch, finally, empty

// 5. Observe WorkshopObservables.RandomIntegers two times and print out when the sum of 2 latest values produced by these observables is dividable by 7.
//    TIPS: zip













































































































//SOLUTIONS
var Solutions = function () {

    // 1. Observe WorkshopObservables.AnswerToLife and print out the answer
    WorkshopObservables.AnswerToLife.subscribe(function (i) { console.log('The answer is: %s', i) });


    // 2. Print out the current datetime every second 3 times
    //    TIPS: interval, map, take
    Rx.Observable.interval(1000)
        .map(new Date())
        .take(3)
        .subscribe(function (i) { console.log(i) });


    //3 . Observe WorkshopObservables.RandomIntegers. Take 10 values, print each value and finally print their sum
    //    TIPS: do, sum
    WorkshopObservables.RandomIntegers
        .take(10)
        .do(function (i) { console.log('Random number is %s', i) })
        .sum()
        .subscribe(function (i) { console.log('The sum of 10 random integers was %s', i) });


    //4. Observe WorkshopObservables.ErrornousStream. When an error occurs, print it out and terminate the observable sequence.
    //TIPS: catch, finally, empty
    var safeStream = WorkshopObservables.ErrornousStream
        .catch(function (e) {
            console.log(e);
            return Rx.Observable.empty();
        })
        .finally(function () {
            console.warn("Stream finished.");
        });

    safeStream.subscribe();


    // 5. Observe WorkshopObservables.RandomIntegers two times and print out when the sum of 2 latest values produced by these observables is dividable by 7.
    //    TIPS: zip
    Rx.Observable.zip(WorkshopObservables.RandomIntegers, WorkshopObservables.RandomIntegers,
        function (a, b) {
            return a + b;
        }).where(function (i) { return i % 7 == 0 })
        .subscribe(function (i) { console.log('%s is dividable by 7', i) });
};



