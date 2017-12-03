/*
    1- create a very large file

*/

console.log(`
       -------------------------------------
            This is an example for stream.
       -------------------------------------
        1- to create big file via stream type "create"
        2- to serve that big file type "serve"
        3- to see example of writable stream type "echo"
        4- to see example of readable stream type "read"
        5- to convert the big html file into compress type "compress"

`);

const readline = require('readline');
const fs = require('fs');
const path = require('path');
const fileLocation = path.resolve(__dirname, './files/big.html');

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});

function createBigFile() {
  const file = fs.createWriteStream(fileLocation);
  let index = 0;
  file.write(`<html>
                <head>
                    <title>Big File</title>
                </head>
             <body>
        `);

  while (index++ < 1e6) {
    file.write(`
    <div style="border:1px dashed gray;padding:10px 10px;margin:5px 5px">
    <h3>Generating - ${index + 1}</h3
    <p>
    Lorem Ipsum is simply dummy text of the printing and typesetting industry.
    Lorem Ipsum has been the industry 's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
   </p>
    </div>
    `);
  }
  file.write('</body></html>');
  file.end();
}

function startServer() {
  const server = require('http').createServer();
  const port = 8000;

  server.on('request', (req, res) => {
    // this is not a good approach
    // fs.readFile(fileLocation, (err, data) => {
    //   if (err) throw err;
    //   res.end(data);
    // });

    // better approach would be this : as this will chunk data as stream.
    // above everyhing was in the buffer.
    const src = fs.createReadStream(fileLocation);
    src.pipe(res);
  });
  console.log('starting server on:', port);
  server.listen(port);
  console.log('server started on : http://localhost:8000/');
}

function simpleEcho() {
  //   const {Writable} = require('stream');

  //   const echoStream = new Writable({
  //     write(chuck, encoding, callback) {
  //       console.log("\n Echo :",chuck.toString());
  //       callback();
  //     },
  //   });

  //   process.stdin.pipe(echoStream);

  // above code can be written as :
  process.stdin.pipe(process.stdout);
}

function simpleRead() {
  const {Readable} = require('stream');
  const readStream = new Readable({
    read(size) {
      setTimeout(() => {
        if (this.char >= 90) {
          this.push(null);
          return;
        }
        this.push(String.fromCharCode(this.char++));
      }, 1000);
    },
  });

  readStream.char = 65;
  readStream.pipe(process.stdout);
}

function transform() {
  const zip = require('zlib');
  fs
    .createReadStream(fileLocation)
    .pipe(zip.createGzip())
    .on('data', () => {
      process.stdout.write('.');
    })
    .pipe(fs.createWriteStream(fileLocation + '.gz'))
    .on('finish', () => {
      console.log('file has been compressed');
    });
}

(function startProgram() {
  rl.question('What do you want to do ?', ans => {
    switch (ans.toLocaleLowerCase()) {
      case 'create':
      case '1':
        console.time('createfile:');
        createBigFile();
        console.timeEnd('createfile:');
        break;
      case 'serve':
      case '2':
        console.time('startserver');
        startServer();
        console.timeEnd('startserver');
        break;
      case 'echo':
      case '3':
        console.log('Creating Echo ....');
        console.time('echo');
        simpleEcho();
        console.timeEnd('echo');
        break;
      case 'read':
      case '4':
        console.log('Creating Read ....');
        console.time('read');
        simpleRead();
        console.timeEnd('read');
        break;
      case 'compress':
      case '5':
        console.log('Creating Compress ....');
        console.time('compress');
        transform();
        console.timeEnd('compress');
        break;
      default:
        console.log('wrong ans');
        process.exit(1);
        break;
    }
    startProgram();
  });
})();
