module.exports = function (context, req) {
    context.log('JavaScript Odd or Even triggered - HTTP.');

    if (req.query.number && parseInt(req.query.number)) {
        context.res = {
            status: 200,
            body: parseInt(req.query.number) % 2 == 0 ? "Even" : "Odd"
        }
    }
    else {
        context.res = {
            status: 400,
            body: `Unable to parse the query parameter 'number'. Got value: ${req.query.number}`
        }
    }
    
    context.done();
};