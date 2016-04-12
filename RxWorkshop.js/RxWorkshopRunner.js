// Reference main Rx
var Rx = require('rx');

Rx.Observable.just('Welcome to Rx workshop!')
    .subscribe(function (i) { console.log(i) });

process.stdin.setRawMode(true);
process.stdin.resume();
process.stdin.on('data', process.exit.bind(process, 0));

