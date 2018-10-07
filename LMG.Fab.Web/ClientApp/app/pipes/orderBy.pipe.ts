import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'orderBy',
    pure: false
})
export class OrderPipe implements PipeTransform {


    static isString(value: any) {
        return typeof value === 'string' || value instanceof String;
    }

    static defaultCompare(a: any, b: any, aId: any, bId: any) {
        if (a === b) {
            if (aId && bId) {
                return aId > bId ? 1 : -1;
            }
            return 0;
        }
        if (a == null) {
            return 1;
        }
        if (b == null) {
            return -1;
        }
        if (OrderPipe.isString(a) && OrderPipe.isString(b)) {
            return a.toLowerCase() > b.toLowerCase() ? 1 : -1;
        }
        return a > b ? 1 : -1;
    }

    static parseExpression(expression: string): string[] {
        expression = expression.replace(/\[(\w+)\]/g, '.$1');
        expression = expression.replace(/^\./, '');
        return expression.split('.');
    }

    static getValue(object: any, expression: string[]) {
        for (let i = 0, n = expression.length; i < n; ++i) {
            const k = expression[i];
            if (!(k in object)) {
                return;
            }
            object = object[k];
        }

        return object;
    }

    transform(value: any | any[], idColumn: any, expression?: any, reverse?: boolean): any {
        if (!value) {
            return value;
        }

        if (Array.isArray(value)) {
            return this.sortArray(value, idColumn, expression, reverse);
        }

        return value;
    }


    private sortArray(value: any[], idColumn: any, expression?: any, reverse?: boolean): any[] {
        const isDeepLink = expression && expression.indexOf('.') !== -1;

        if (isDeepLink) {
            expression = OrderPipe.parseExpression(expression);
        }

        let compareFn: Function;

        compareFn = OrderPipe.defaultCompare;

        let array: any[] = value.sort((a: any, b: any): number => {
            if (!expression) {
                return compareFn(a, b);
            }

            if (!isDeepLink) {
                if (a && b) {
                    return compareFn(a[expression], b[expression], a[idColumn], b[idColumn]);
                }
                return compareFn(a, b);
            }

            return compareFn(OrderPipe.getValue(a, expression), OrderPipe.getValue(b, expression));
        });

        if (reverse) {
            return array.reverse();
        }

        return array;
    }
}