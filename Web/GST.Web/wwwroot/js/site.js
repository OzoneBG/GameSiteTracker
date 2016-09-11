function getCurrentAction()
{
    var wholeUrl = window.location.href;
    var splits = wholeUrl.split('/', 5);

    var currentAction = splits[splits.length - 1];

    return currentAction;
}