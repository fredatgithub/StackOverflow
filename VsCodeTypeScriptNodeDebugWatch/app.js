var request = require('request'), dom = require('node-dom').dom;
var req = { uri: "http://www.bigfont.ca" };
request(req, function (error, response, page) {
    if (!error && response.statusCode == 200) {
        var options = {
            url: "http://www.bigfont.ca",
            features: {
                FetchExternalResources: { script: '', img: '', input: '', link: '' },
                ProcessExternalResources: { script: '', img: '', link: '', input: '' },
                removeScript: true //Remove scripts for innerHTML and outerHTML output
            }
        };
        window = dom(page, null, options); //global
        document = window.document; //global
        document.onload = function () {
        };
    }
    ;
});
//# sourceMappingURL=app.js.map