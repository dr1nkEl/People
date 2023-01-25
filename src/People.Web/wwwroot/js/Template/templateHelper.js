let counter = 0;
let questionAnswerTypes;
let optionType;

/**
 * Function to get questions.
 * @function
 * @param {string} questionClassName - Question class name.
 */
function getQuestions(questionClassName) {
    const questions = document.querySelectorAll('.' + questionClassName);
    const result = [];
    for (const elem of questions) {
        const title = elem.getElementsByClassName('questionTitle')[0].value;
        const order = elem.getElementsByClassName('questionOrder')[0].value;

        const type = elem.getElementsByClassName('questionType')[0].value;

        const question = {
            Title: title,
            Order: order,
            Options: [],
            AnswerType: type
        };

        if (type != optionType) {
            result.push(question);
            continue;
        }

        const options = elem.getElementsByClassName('questionOption');

        for (let optionId = 0; optionId < options.length; optionId++) {
            const optionDom = options[optionId];
            const optionTitle = optionDom.getElementsByClassName('optionTitle')[0].value;;
            const optionOrder = optionDom.getElementsByClassName('optionOrder')[0].value
            question['Options'].push({ Title: optionTitle, Order: optionOrder });
        }
        result.push(question);
    }
    return result;
}

/**
 * Removes self.
 * @param {string} id - DOM ID to be removed.
 */
function removeSelf(id) {
    $('#' + id).remove();
}

/**
 * Function to get question answer attributes
 * @function
 * @async
 * @returns {Any} - Array of {text: "string", value: number}
 **/
async function getQuestionAnswerTypes() {
    questionAnswerTypes = await $.ajax({
        method: 'GET',
        async: true,
        url: '/api/PR/GetQuestionAnswerTypes',
        datatype: 'json',
    });
    optionType = questionAnswerTypes.filter(x => x.text == 'Options')[0].value;
}

/**
 * Function to create attribute option input.
 * @param {string} parentId - DOM parent ID.
 * @param {string} name - Name for created input.
 * @param {any} data - Initial data with format: [{id: number, answerType: int, title: "string", order: int, options: [{id: number, title: "string", order: number}]}].
 */
function addQuestionInput(parentId, className, data) {
    const id = parentId + counter;

    counter += 1;

    const optionBlockId = 'optionBlock'+counter;

    $('#' + parentId).append('<div class="row mx-auto p-2 ' + className + '" id="' + id + '">' +
        '<label class="row mx-auto ">Question</label>' +
        '<input class="form-control col questionTitle questionTitle'+counter+'" placeholder = "Title" " required/>' +
        '<input class="form-control col questionOrder questionOrder'+counter+'" type="number" placeholder="Order" required/>' +
        '<a class="col btn btn-danger" id="delete' + id + '">X</a>' +
        '<div class="row mx-auto">'+
        '<label class="col" for="answerTypeSelector' + counter + '">Answer type</label>' +
        '<select data-counter="'+counter+'" class="form-select col questionType questionType'+counter+'" id="answerTypeSelector' + counter + '"></select>' +
        '</div>'+
        '<div id="'+optionBlockId+'"><button onclick="addQuestionOption(\'optionBlock'+counter+'\', null);" class="btn btn-primary" type="button">Add Option</button></div></div>');
    $('#delete' + id).on('click', function () {
        $('#' + id).remove();
    });

    $.each(questionAnswerTypes, function (itemNumber) {
        const item = questionAnswerTypes[itemNumber];
        $('#answerTypeSelector' + counter).append('<option value="' + item.value + '">' + item.text + '</option>');
    })

    $('#answerTypeSelector' + counter).on('change', function () {
        const value = $(this).val();
        const optValue = questionAnswerTypes.filter(item => item.text == 'Options')[0].value;
        const counted = $(this).data('counter');
        if (value == optValue) {
            $('#optionBlock' + counted).removeAttr('hidden');
        }
        else {
            $('#optionBlock' + counted).attr('hidden', 'hidden');
        }
    });

    if (data) {
        for (let i = 0; i < data.length; i++) {
            $('.questionTitle' + counter)[0].value = data[i].title;
            $('.questionOrder' + counter)[0].value = data[i].order;
            $('.questionType' + counter)[0].value = data[i].answerType;
            for (let y = 0; y < data[i].options.length; y++){
                addQuestionOption(optionBlockId, data[i].options[y]);
            }
        }
    }

    $('#answerTypeSelector' + counter).trigger('change');
};

/**
 * Function to add question option.
 * @param {string} parentId - DOM parent ID.
 * @param {any} data - Initial data of type {id: number, title: "string", order: number}.
 */
function addQuestionOption(parentId, data) {
    const id = parentId + counter;

    $('#' + parentId).append('<div id="' + id + '" class="questionOption row w-100 mx-auto" ">' +
        '<label class="row mx-auto">Option</label>'+
        '<input class="optionTitle form-control col" placeholder = "Title" required>'+
        '<input type="number" class="optionOrder col form-control" placeholder="Order" required/>'+
        '<a class="col btn btn-secondary" id="delete' + id + '">X</a></div >');
    $('#delete' + id).on('click', function () {
        $('#' + id).remove();
    });

    if (data) {
        $('#' + id + ' .optionTitle')[0].value = data.title;
        $('#' + id + ' .optionOrder')[0].value = data.order;
    }
    counter += 1;
}

/**
 * Get template.
 * Question - {id: number, answerType: int, title: "string", order: int, options: [{id: number, title: "string", order: number}]}
 * @async
 * @function
 * @param {number} id - Template ID.
 * @returns {any} Template -  Request data - {id: number?, Name: "string", RelatedPositionId: number, ReviewTypeId: number, ReviewedUserQuestions: Question[], FeedBackQuestions: Question[]}.
 */
async function getTemplate(id) {
    return await $.ajax({
        method: 'GET',
        url: '/api/PR/GetTemplate',
        data: { id: id },
        datatype: 'json',
    });
}

/**
 * Get request data.
 * Question - is object {id: number, answerType: int, title: "string", order: int, options: [{id: number, title: "string", order: number}]}
 * @function
 * @param {number} id - Id of template, not required.
 * @returns {any} Request data - {id: number?, Name: "string", RelatedPositionId: number, ReviewTypeId: number, ReviewedUserQuestions: Question[], FeedBackQuestions: Question[]}.
 */
function getTemplateRequestData(id) {
    const templateName = $('#templateName').val();
    const position = $('#positionSelector').val();
    const reviewType = $('#reviewTypeSelector').val();

    const requestData = {
        'Id': id,
        'Name': templateName,
        'RelatedPositionId': position,
        'ReviewTypeId': reviewType,
        'ReviewedUserQuestions': [],
        'FeedbackQuestions': [],
    }

    requestData['ReviewedUserQuestions'].push(...getQuestions('reviewedQuestion'));
    requestData['FeedbackQuestions'].push(...getQuestions('feedbackQuestion'));

    return requestData;
}

/**
 * Function to call PATCH template method.
 * Question - is object {id: number, answerType: int, title: "string", order: int, options: [{id: number, title: "string", order: number}]}
 * @async
 * @function
 * @param {any} requestData - {id: number, Name: "string", RelatedPositionId: number, ReviewTypeId: number, ReviewedUserQuestions: Question[], FeedBackQuestions: Question[]}.
 */
async function patchTemplate(requestData) {
    await $.ajax({
        method: 'PATCH',
        url: '/api/PR/PatchReviewTemplate',
        data: { model: requestData },
        datatype: 'json',
    });
}

/**
 * Function to create new review template.
 * @async
 * @function
 * @param {any} requestData - {Name: "string", RelatedPositionId: number, ReviewTypeId: number, ReviewedUserQuestions: Question[], FeedBackQuestions: Question[]}.
 */
async function createNew(requestData) {
    await $.ajax({
        method: 'POST',
        url: '/api/PR/NewTemplate',
        data: { model: requestData },
        datatype: 'json',
    });
}

/**
 * Filling view with template fields.
 * Question - is object {id: number, answerType: int, title: "string", order: int, options: [{id: number, title: "string", order: number}]}
 * @param {any} template - {id: number?, Name: "string", RelatedPositionId: number, ReviewTypeId: number, ReviewedUserQuestions: Question[], FeedBackQuestions: Question[]}.
 */
function fillFields(template) {
    $.each($('.feedbackQuestion'), function () {
        $(this).remove();
    });
    $.each($('.reviewedQuestion'), function () {
        $(this).remove();
    })
    $('#templateName').val(template.name);
    $('#positionSelector').val(template.relatedPositionId);
    $('#reviewTypeSelector').val(template.reviewTypeId);
    $.each(template.feedbackQuestions, function(){
        addQuestionInput('feedbackQuestionBlock', 'feedbackQuestion', $(this));
    })
    $.each(template.reviewedUserQuestions, function(){
        addQuestionInput('reviewedQuestionBlock', 'reviewedQuestion', $(this));
    })
}
