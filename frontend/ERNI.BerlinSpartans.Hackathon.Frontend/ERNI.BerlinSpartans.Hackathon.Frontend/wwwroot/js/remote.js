$(document).ready(function () {
    $(window).on('keydown', function (e) {
        switch (e.keyCode) {
            case 16: { //Shift
                var ele = $('#button-accelerate');
                toggleState('Shift', ele, true);
                break;
            }
            case 81: { //Q
                var ele = $('#button-turn-head-right');
                toggleState('Q', ele, true);
                break;
            }
            case 87: { //W
                var ele = $('#button-forward');
                toggleState('W', ele, true);
                break;
            }
            case 69: { //E
                var ele = $('#button-turn-head-left');
                toggleState('E', ele, true);
                break;
            }
            case 17: { //Ctrl
                var ele = $('#button-decelerate');
                toggleState('Ctrl', ele, true);
                break;
            }
            case 65: { //A
                var ele = $('#button-left');
                toggleState('A', ele, true);
                break;
            }
            case 83: { //S
                var ele = $('#button-backward');
                toggleState('S', ele, true);
                break;
            }
            case 68: { //D
                var ele = $('#button-right');
                toggleState('D', ele, true);
                break;
            }
        }
    });
    $(window).on('keyup', function (e) {
        switch (e.keyCode) {
            case 16: { //Shift
                var ele = $('#button-accelerate');
                toggleState('Shift', ele, false);
                break;
            }
            case 81: { //Q
                var ele = $('#button-turn-head-right');
                toggleState('Q', ele, false);
                break;
            }
            case 87: { //W
                var ele = $('#button-forward');
                toggleState('W', ele, false);
                break;
            }
            case 69: { //E
                var ele = $('#button-turn-head-left');
                toggleState('E', ele, false);
                break;
            }
            case 17: { //Ctrl
                var ele = $('#button-decelerate');
                toggleState('Ctrl', ele, false);
                break;
            }
            case 65: { //A
                var ele = $('#button-left');
                toggleState('A', ele, false);
                break;
            }
            case 83: { //S
                var ele = $('#button-backward');
                toggleState('S', ele, false);
                break;
            }
            case 68: { //D
                var ele = $('#button-right');
                toggleState('D', ele, false);
                break;
            }
        }
    });
});
function toggleState(key, ele, pressed) {
    if (pressed) {
        var oldSrc = $(ele).children('img').attr('src');
        var extensionIndex = oldSrc.lastIndexOf('.');
        var newSrc = oldSrc.substring(0, extensionIndex) + "-pressed" + oldSrc.substring(extensionIndex);
        $(ele).children('img').attr('src', newSrc);
    }
    else {
        var newSrc = $(ele).children('img').attr('src').replace('-pressed', '');
        $(ele).children('img').attr('src', newSrc);
    }
}