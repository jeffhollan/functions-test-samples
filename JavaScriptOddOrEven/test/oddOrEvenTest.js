var assert = require('assert');
var oddOrEven = require('../OddOrEven/index');

var context;

describe('OddOrEven', function() {
  before(function() {
    context = getContextObject();
  });
    describe('IsEven', function() {
        it('should return even', function() {     
            context.done = function() {
                assert.equal(this.res.status, 200);
                assert.equal(this.res.body, "Even");
            }
            oddOrEven(context, { query: {number: "2"}});
        });
  });
    describe('IsOdd', function() {
        it('should return odd', function () {
            context.done = function() {
                assert.equal(this.res.status, 200);
                assert.equal(this.res.body, "Odd");
            }
            oddOrEven(context, { query: {number: "3"}});
    });
  });
    describe('IsNeither', function() {
        it('should return error', function() {
            context.done = function() {
                assert.equal(this.res.status, 400);
                assert.equal(this.res.body, "Unable to parse the query parameter 'number'. Got value: I'm Even");
            }
            oddOrEven(context, { query: {number: "I'm Even"}});
        });
    });
});

function getContextObject() {
    return {
        res: null,
        log: function() {
            console.log(arguments[0]);
        },
        done: null
    }
}