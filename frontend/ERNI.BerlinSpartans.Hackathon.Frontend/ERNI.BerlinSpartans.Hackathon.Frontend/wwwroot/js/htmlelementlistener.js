class HtmlElementListener {
    classSelector = "";
    //Tracking the movement of the robot.
    movement = {
        forward: false,
        backward: false,
        left: false,
        right: false,
        turnHeadLeft: false,
        turnHeadRight: false
    };

    constructor(classSelector) {
        this.classSelector = classSelector;
        this.addKeyDownEventListeners();
        this.addKeyUpEventListeners();
    }

    //Select buttons used for controlling the robot and add listeners to them so we can track movement.
    //This method dispatches triggers which can be listened for on the other side.
    addKeyDownEventListeners() {
        var buttonQ = $('.' + this.classSelector + '-Q');
        var buttonE = $('.' + this.classSelector + '-E');
        var buttonW = $('.' + this.classSelector + '-W');
        var buttonA = $('.' + this.classSelector + '-A');
        var buttonS = $('.' + this.classSelector + '-S');
        var buttonD = $('.' + this.classSelector + '-D');
        var self = this;

        $(buttonQ).mousedown(function (e) {
            if (self.movement.turnHeadLeft !== true) {
                self.movement.turnHeadLeft = true;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonE).mousedown(function (e) {
            if (self.movement.turnHeadRight !== true) {
                self.movement.turnHeadRight = true;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonW).mousedown(function (e) {
            if (self.movement.forward !== true) {
                self.movement.forward = true;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonA).mousedown(function (e) {
            if (self.movement.left !== true) {
                self.movement.left = true;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonS).mousedown(function (e) {
            if (self.movement.downd !== true) {
                self.movement.backward = true;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonD).mousedown(function (e) {
            if (self.movement.right !== true) {
                self.movement.right = true;
                $(self).trigger('movementChanged', self.movement);
            }
        });
    }

    //Select buttons used for controlling the robot and add listeners to them so we can track movement.
    //This method dispatches triggers which can be listened for on the other side.
    addKeyUpEventListeners() {
        var buttonQ = $('.' + this.classSelector + '-Q');
        var buttonE = $('.' + this.classSelector + '-E');
        var buttonW = $('.' + this.classSelector + '-W');
        var buttonA = $('.' + this.classSelector + '-A');
        var buttonS = $('.' + this.classSelector + '-S');
        var buttonD = $('.' + this.classSelector + '-D');
        var self = this;

        $(buttonQ).mouseup(function (e) {
            if (self.movement.turnHeadLeft !== false) {
                self.movement.turnHeadLeft = false;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonE).mouseup(function (e) {
            if (self.movement.turnHeadRight !== false) {
                self.movement.turnHeadRight = false;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonW).mouseup(function (e) {
            if (self.movement.forward !== false) {
                self.movement.forward = false;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonA).mouseup(function (e) {
            if (self.movement.left !== false) {
                self.movement.left = false;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonS).mouseup(function (e) {
            if (self.movement.downd !== false) {
                self.movement.backward = false;
                $(self).trigger('movementChanged', self.movement);
            }
        });

        $(buttonD).mouseup(function (e) {
            if (self.movement.right !== false) {
                self.movement.right = false;
                $(self).trigger('movementChanged', self.movement);
            }
        });
    }
}