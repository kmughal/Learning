process.on('message', m => {
  console.log('message from parent', m);
});

let counter = 0;
setInterval(() => {
  process.send({counter: counter++});
}, 1000);
