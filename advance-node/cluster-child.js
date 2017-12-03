const cluster = require('cluster');
const os = require('os');

if (cluster.isMaster) {
  const cpus = os.cpus().length;
  console.log(`Forking ${cpus}`);
  let index = 0;
  while (index++ < cpus) {
    cluster.fork();
  }
  console.dir(cluster.workers, {depth: 0});
  const values = Object.keys(cluster.workers).map(v => cluster.workers[v]);
  values.forEach(worker => {
    worker.send(`Sending message ${worker.id}`);
  });

  // improve the availability
  cluster.on('exit', (worker, code, signal) => {
    if (code !== 0 && !worker.exitedAfterDisconnect) {
      console.log(`worker ${worker.id} just crashed started a new one now!`);
      cluster.fork();
    }
  });
} else if (cluster.isWorker) {
  require('./cluster-server');
}
