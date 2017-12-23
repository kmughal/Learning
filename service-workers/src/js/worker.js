function createPrimeNumberList(end) {
  var start = 0,
    result = ['<ul>'];

  while (start < end) {
    if (isPrime(start)) {
      result.push('<li>' + start + '</li>');
    }
    start++;
  }
  result.push('</ul>');
  return result.join('');
}

function isPrime(num) {
  for (var i = 2; i < num; i++) {
    if (num % i === 0) {
      return false;
    }
  }
  return true;
}

self.addEventListener("message" , function(e) {
    var param = e.data;
    if (param.command === "start") {
        var result = createPrimeNumberList(param.num);
        self.postMessage(result);
    }
})


