# Introduction

## Streams

Streams are collection of data this doesn't mean it shoulld be and must be in
memory it can be sourced from anywhere. When ever you have to stream out a very
large file it is better to use stram object see below code:

```js
    const server = require("http").createServer();

    server.on("request",(req,res)=>{
       cons src = fs.createReadStream(fileLocation /*this variable will hold the location*/);
       src.pipe(res);// by doing so we can send any amount of data
    });
```

In node there are manily 4 types of streams:

* Readable
* Writable
* Duplex ( just like socket)
* Transform

All streams are instances of EventEmitters they emit events. We can pipe calls :

```js
// a | b | c | d can be viewed as in js
a
  .pipe(b)
  .pipe(c)
  .pipe(d); // a,b,c,d are duplex ... means they are raedable and writable
```

### Transform example

this stream is used to perform transformations. Suppose that you want to
compress a very large file in order to do so see below code:

```js
function transform() {
  const zip = require('zlib');
  fs
    .createReadStream(fileLocation)
    .pipe(zip.createGzip())
    .pipe(fs.createWriteStream(fileLocation + '.gz'))
    .on('finish', () => {
      console.log('file has been compressed');
    });
}
```

## Debug

You can debug any js in node by using following commands:

* first open terminal
* then launch node by typing node

```js
    node debug filename.js
```

* in order to get more help you can type help which will display all other
  commands such as setting up break point , stepping in/out or clear the break
  point
* also you can do the debugging in chrome as well for this type the following
  command:

```js
    node --inspect --debug-brk file-name.js
```

once executed successfully it will give you a url which you copy / paste that
url in chrome dev tool to play with script.

## Cluster

Following strategies can be used:

* Clone ( multiple version of your application)
* Decompose( micro-services )
* Splitting / Sharding / Partition ( partition your application based upon lang
  or something else .)

There are four ways to create child process

* spawn(launches the command in the new process;)
* fork(speical case of spawn,a communication channel is present which enable to
  send messages.each spwn will have it's own resources so it is not recommended
  to open large no. of spawns.)
* execFile(same as exec apart from that you specify a file.)
* exec(spawn a shell and then executes commands.)

### Cluster
This allows us to fork our main application as many times as we want. A master process receive requests then decide which worker process will handle this. It uses Round robin algo. apart from windows (this can be modified). It is the most basic load balancing algo. You can do a benchmark test by using apache tool.
You can use the following command:
```sh
ab -n 100 -c 10 http://localhost:8000/
```

Below is the benchmark test it can varry depending upon the machine you are running.
As you can see that Requests per second indicates how many requests were handled. Surely there is
a gain which we can see.

```html

ab -n 100 -c 10 http://localhost:8000/
This is ApacheBench, Version 2.3 <$Revision: 1757674 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient).....done


Server Software:
Server Hostname:        localhost
Server Port:            8000

Document Path:          /
Document Length:        24 bytes

Concurrency Level:      10
Time taken for tests:   0.761 seconds
Complete requests:      100
Failed requests:        0
Total transferred:      9900 bytes
HTML transferred:       2400 bytes
Requests per second:    131.42 [#/sec] (mean)
Time per request:       76.091 [ms] (mean)
Time per request:       7.609 [ms] (mean, across all concurrent requests)
Transfer rate:          12.71 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.8      0       6
Processing:    31   74  22.0     65     173
Waiting:        0   17  21.9      6     114
Total:         31   74  22.0     65     173

Percentage of the requests served within a certain time (ms)
  50%     65
  66%     77
  75%     81
  80%     86
  90%    111
  95%    120
  98%    149
  99%    173
 100%    173 (longest request)

 ````

 For process manager you can use http://pm2.keymetrics.io/