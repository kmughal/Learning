const fs = require('fs');
const path = require('path');

const activate = function() {
  const filesDirectory = path.resolve(__dirname, 'files');
  const out = fs.createWriteStream(
    path.resolve(__dirname, './logs/application.log')
  );
  const err = fs.createWriteStream(path.resolve(__dirname, './logs/err.log'));
  const console2 = new console.Console(out, err);

  fs.watch(filesDirectory, 'utf-8', (event, filename) => {
    console2.info(`Action:${event}, File:${filename}`);
  });
};

module.exports = activate;
