class KeyBoardListener {
    movement = {
        forward: false,
        backward: false,
        left: false,
        right: false,
        accelerate: false,
        decelerate: false,
        turnHeadLeft: false,
        turnHeadRight: false
    };

    constructor() {
        this.addKeyDownEventListener();
        this.addKeyUpEventListener();
    }


    addKeyDownEventListener() {
        var self = this;
        $(document).on('keydown', function (e) {
            switch (e.key.toUpperCase()) {
                case 'SHIFT':
                    if (self.movement.accelerate !== true) {
                        self.movement.accelerate = true;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'CONTROL':
                    if (self.movement.decelerate !== true) {
                        self.movement.decelerate = true;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
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
            }
        });
    }

    addKeyUpEventListener() {
        var self = this;
        $(document).on('keyup', function (e) {
            switch (e.key.toUpperCase()) {
                case 'SHIFT':
                    if (self.movement.accelerate !== false) {
                        self.movement.accelerate = false;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
                case 'CONTROL':
                    if (self.movement.decelerate !== false) {
                        self.movement.decelerate = false;
                        $(self).trigger('movementChanged', self.movement);
                    }
                    break;
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