export class ConnectionEvent {
    isConnected?: boolean;
    dateTime?: Date;

    constructor(obj: Partial<ConnectionEvent> = {}) {
        Object.assign(this, obj);
    }
}