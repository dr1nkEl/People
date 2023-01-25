let counter = 0;

/**
 * Function to create attribute option input.
 * @param {string} parentId - DOM parent ID.
 * @param {string} name - Name for created input.
 * @param {string} title - Title for input.
 */
function addAttributeOptionInput(parentId, name, title) {
    const id = parentId + counter;
    counter += 1;

    $('#' + parentId).append('<div id="' + id + '"><input value="'+title+'" placeholder="Attribute title" name="' + name + '"><a id="delete' + id + '">X</a></div>');
    $('#delete' + id).on('click', function () {
        $('#' + id).remove();
    });
};

/**
 * Function to show options.
 * @param {number} dropDownId - ID of dropdown.
 * @param {string} attributeOptionsId - Attribute options ID.
 * @param {string} typeSelectorId - Type selector ID.
 */
function showOptions(dropDownId, attributeOptionsId, typeSelectorId) {
    const attributeType = $('#' + typeSelectorId).val();
    if (dropDownId == attributeType) {
        $('#' + attributeOptionsId).removeAttr('hidden');
    }
    else {
        $('#' + attributeOptionsId).attr("hidden", "hidden");
    }
};

/**
 * Removes self.
 * @param {string} id - DOM ID to be removed.
 */
function removeSelf(id) {
    $('#' + id).remove();
}

/**
 * Function to get attribute options with ajax.
 * @function
 * @async
 * @param {number} attributeId - Attribute ID to get options of.
 * @return {any} - Array of attribute options.
 */
async function getAttributeOptions(attributeId) {
    return $.ajax({
        method: 'GET',
        url: '/api/Attribute/GetAttributeOptions',
        data: { attributeId: attributeId},
        datatype: 'json',
        async: true,
        error: function (response) {
            console.log(response);
        }
    });
}

/**
 * Function to set default attribute options for attribute.
 * @async
 * @function
 * @param {number} attributeId - Attribute ID.
 * @param {string} parentId - DOM parent ID.
 * @param {string} name - Name for created input.
 */
async function setDefaultAttributeOptions(attributeId, parentId, name) {
    const options = await getAttributeOptions(attributeId);
    $.each(options, function () {
        const elem = $(this)[0];
        addAttributeOptionInput(parentId, name, elem.title);
    })
}
