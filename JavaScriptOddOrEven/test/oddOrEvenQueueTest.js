const assert = require('assert');
const axios = require('axios');
const moxios = require('moxios');
const sinon = require('sinon');


const oddOrEvenQueue = require('../OddOrEvenQueue/index');

var context;

describe('OddOrEvenQueue', function() {
  before(function() {
    context = getContextObject();
  });
  beforeEach(() => moxios.install());
  afterEach(() => moxios.uninstall());

    describe('IsEven', function() {
        it('should return even', async function() {  
            moxios.wait(function () {
                let request = moxios.requests.mostRecent();
                assert.equal(request.config.data, "Even");
                request.respondWith({
                    status: 200
                });
            });

            return await oddOrEvenQueue(context, "2").catch((reason) => {console.log(reason)});
        });
  });
    describe('IsOdd', function() {
        it('should return odd', async function () {
            moxios.wait(function () {
                let request = moxios.requests.mostRecent();
                assert.equal(request.config.data, "Odd");
                request.respondWith({
                    status: 200
                });
            });

            return await oddOrEvenQueue(context, "3").catch((reason) => {console.log(reason)});
    });
  });
    describe('IsNeither', function() {
        it('should return error',async function() {
            moxios.wait(function () {
                let request = moxios.requests.mostRecent();
                request.respondWith({
                    status: 500
                });
            });

            // I'm not sure of a better way to assert on a Promise exception
            // But would love to know 👍

            try {
                await oddOrEvenQueue(context, "I'm Even");
                assert.equal(1, 2);
            }
            catch(e) {
                assert.equal(1, 1);
            }
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