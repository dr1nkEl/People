let options;

/**
 * Function to get interval types.
 * @async
 * @function
 * @returns {Any} - [{Text: "string", Value: number}].
 * */
async function getIntervalTypes() {
    options = await $.ajax({
        method: 'GET',
        async: true,
        url: '/api/PR/GetIntervalTypes',
        datatype: 'json',
    });
}

/**
 * Function to fill selector.
 * @async
 * @function
 * @param {string} selectorId - DOM selector ID.
 */
function fillSelector(selectorId){
    $.each(options, function (itemNumber) {
        const item = options[itemNumber];
        $('#'+selectorId).append('<option value="' + item.value + '">' + item.text + '</option>');
    })
}
