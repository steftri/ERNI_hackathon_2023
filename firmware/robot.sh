#! /bin/sh
### BEGIN INIT INFO
# Provides: noip
# Required-Start: $syslog
# Required-Stop: $syslog
# Default-Start: 2 3 4 5
# Default-Stop: 0 1 6
# Short-Description: noip server
# Description:
### END INIT INFO
 
case "$1" in
    start)
        echo "Starting robot firmware"
        # Starte Programm
        sudo python /home/erni/firmware/main.py
        ;;
    stop)
        echo "Stopping robot firmware"
        # Beende Programm
        killall python
        ;;
    *)
        echo "Usage: /etc/init.d/robot {start|stop}"
        exit 1
        ;;
esac
 
exit 0