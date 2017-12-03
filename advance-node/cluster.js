function spawnExample() {
  const {spawn} = require('child_process');

  const child = spawn('ls');

  child.stdout.on('data', data => {
    console.log(data.toString());
  });
  child.stderr.on('error', err => {
    console.log(err);
  });
  child.on('exit', (code, signal) => {
    console.log('code:', code, 'signal:', signal);
  });

  // other events are message , disconnect,close,error
}

function execExample() {
  const {exec} = require('child_process');

  exec('ls', (err, stdout, stderr) => {
    if (err) {
      console.log(err);
      return;
    }
    console.log('Command Result >\n', stdout);
  });
}
// exec is shell style
// it will buffer the whole data before returning

function forkExample() {
  const {fork} = require('child_process');
  const path = require('path');
  const childJSPath = path.resolve(__dirname, 'child.js');
  const forked = fork(childJSPath);

  forked.on('message', m => {
    console.log('message from child', m);
  });

  forked.send({hello: 'hello'});
}

function clusterExample() {
  // 1. build the server

  const http = require('http');
  const pid = process.pid;

  http
    .createServer((req, res) => {
      for (let i = 0; i < 1e6; i++) {
        res.end(`handled by process ${pid}`);
      }
    })
    .listen(8000, () => {
      console.log(`process started ${pid}`);
    });
}
clusterExample();
