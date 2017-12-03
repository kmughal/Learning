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

process.on('message', m => {
  console.log('Message from server', m);
});

// setTimeout(() => process.exit(1), Math.random() * 1000);
