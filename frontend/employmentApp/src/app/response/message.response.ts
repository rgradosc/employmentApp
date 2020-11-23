import { MessageError } from './message.error';
import { MessageOk } from './message.ok';

export class MessageResponse {
    constructor(public messageOk: MessageOk,
                public messageError: MessageError,
                public data: any) { }
}
