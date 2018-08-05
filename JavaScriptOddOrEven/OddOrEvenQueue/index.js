const axios = require('axios');

module.exports = async function (context, myNumber) {
    context.log('JavaScript Odd or even trigger fired - Queue');

    if (myNumber && parseInt(myNumber)) {
        if(parseInt(myNumber) % 2 == 0) {
            context.log('Was even');
            await axios.post('https://importantapi.com/api/transaction','Even');
        } 
        else {
            context.log('Was odd');
            await axios.post('https://importantapi.com/api/transaction', 'Odd');
        }
    }
    else {
        throw new Error(`Unable to parse the queue message. Got value: ${myNumber}`)
    }
};