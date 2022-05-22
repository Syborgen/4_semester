#include <iostream>
#include <math.h>
using namespace std;

int main()
{
    setlocale(LC_ALL, "rus");
    float x, y,z, rCPP, rASM,zero = 0;
    int  err = 0,five=5,three=3;
    float e = 2.7;

    cout << "Введите x: ";
    cin >> x;
    cout << "Введите y: ";
    cin >> y;
    cout << "Введите z: ";
    cin >> z;

    if (z > 0)
        //rCPP = sin(y) / sqrt(z) + (pow(cos(x + y), 2) * z);
        rCPP = (pow(e,z))/5 - cos(y/x)/3-log10(z+y);
    
        
    _asm
    {
        fld z                      ; z
        fld e                       ; z, e
        fyl2x                       ; z* log2(e)
        FLD st                      ; z* log2(e), y* log2(e)
        FRNDINT                     ; z* log2(e), A
        FSUB st(1), st              ; B, A
        FXCH                        ; A, B
        F2XM1                       ; A, e^ B - 1
        FLD1                        ; A, e^ B - 1, 1
        FADDP st(1), st             ; A, e^ B
        FSCALE                      ; A, e^ A* e^ B = e ^ z
        FSTP st(1)                  ; e^ z
        fild five                   ; e^ z, 5
        fdivp st(1), st             ; e^ z / 5
        fld y                       ; e^ z / 5, y
        fld zero                    ; e^ z / 5, y, 0
        FCOMIP  st, st(1)           ; sin(y), z
        jae error
        fld x                       ; e^ z / 5, y, x
        fdivp st(1), st             ; e^ z / 5, y / x
        fcos                        ; e^ z / 5, cos(y / x)
        fild three                  ; e^ z / 5, cos(y / x), 3
        fdivp st(1), st             ; e^ z / 5, cos(y / x) / 3
        fsubp st(1), st             ; e^ z / 5 - cos(y / x) / 3
        fldlg2                      ; e^ z / 5 - cos(y / x) / 3, lg(2)
        fld z                      ; e^ z / 5 - cos(y / x) / 3, lg(2), z
        fld y                       ; e^ z / 5 - cos(y / x) / 3, lg(2), z, y
        faddp st(1), st             ; e^ z / 5 - cos(y / x) / 3, lg(2), z + y
        fyl2x                       ; e^ z / 5 - cos(y / x) / 3, lg(z + y)
        fsubp st(1), st             ; e^ z / 5 - cos(y / x) / 3 - lg(z + y)
        FST rASM;
        jmp END; пропускаем ошибку;
    error:; ошибка
        mov err, 1; флаг ошибки = 1;
    END:; конец модуля

    }

    if (err == 0)
    {
        cout << "Результат С++: " << rCPP << endl;
        cout << "Результат ASM: " << rASM << endl;
    }
    else
    {
        cout << "Ошибка! Z <=0 " << endl;
    }
}
