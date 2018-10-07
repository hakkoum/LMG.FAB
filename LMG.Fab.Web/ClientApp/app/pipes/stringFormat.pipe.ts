import { Pipe, PipeTransform } from '@angular/core';


@Pipe({ name: 'StringFormat' })
export class StringFormat implements PipeTransform {
    transform(input : any, args?: any) {
        if (args.length > 0) {
            var str = input;
            for (var i = 0; i < args.length; i++) {
                var reg = "{" + (i) + "}";
                str = str.replace(reg, args[i]);
            }
            return str;
        }
        return input;
    }
}