class KeyBoardListener {
    //Tracking the movement of the robot.
    movement = {
        forward: false,
        backward: false,
        left: false,
        right: false,
        turnHeadLeft: false,
        turnHeadRight: false,
        startLane: false
    };

    constructor() {
        this.addKeyDownEventListener();
        this.addKeyUpEventListener();
    }

    //Add listener to keyboard events so we can track whether user presses any buttons.
    //This method dispatches triggers which can be listened for on the other side.
    addKeyDownEventListener() {
        var self = this;
        $(document).on('keydown', function (e) {
            switch (e.key.toUpperCase()) {
                case 'Q':
                    if (self.movement.turnHeadLeft !== true) {
                        self.movement.turnHeadLeft = true;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'E':
                    if (self.movement.turnHeadRight !== true) {
                        self.movement.turnHeadRight = true;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'W':
                    if (self.movement.forward !== true) {
                        self.movement.forward = true;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'A':
                    if (self.movement.left !== true) {
                        self.movement.left = true;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'S':
                    if (self.movement.backward !== true) {
                        self.movement.backward = true;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'D':
                    if (self.movement.right !== true) {
                        self.movement.right = true;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'SPACE':
                    if (self.movement.startLane !== true) {
                        self.movement.startLane = true;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
            }
        });
    }

    //Add listener to keyboard events so we can track whether user presses any buttons.
    //This method dispatches triggers which can be listened for on the other side.
    addKeyUpEventListener() {
        var self = this;
        $(document).on('keyup', function (e) {
            switch (e.key.toUpperCase()) {
                case 'Q':
                    if (self.movement.turnHeadLeft !== false) {
                        self.movement.turnHeadLeft = false;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'E':
                    if (self.movement.turnHeadRight !== false) {
                        self.movement.turnHeadRight = false;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'W':
                    if (self.movement.forward !== false) {
                        self.movement.forward = false;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'A':
                    if (self.movement.left !== false) {
                        self.movement.left = false;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'S':
                    if (self.movement.backward !== false) {
                        self.movement.backward = false;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'D':
                    if (self.movement.right !== false) {
                        self.movement.right = false;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
            }
        });
    }
}