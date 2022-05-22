#include <iostream>
#include <cstdlib> // для system
using namespace std;

int main()
{
    setlocale(LC_ALL, "Russian");
    int x[6] = {-3,6,8,20,25,43};
    int xx;//переменная для передачи значения в ассемблерную вставку
    int y;
    
    for (int i = 0; i < 6; i++) {
        cout << "Test №" << i+1 << ": x = " << x[i] << " ---------------------------------------" << endl;
        
        cout << " в C++ ";
        if (x[i] <= 20) {
            y = -3 * pow(x[i], 2);
            cout << "y = -3 * x**2";
        }
        else {
            y = -1200 / x[i];//целочисленное деление
            cout << "y = -1200 / x";
        }
        cout << " = " << y << endl;
        y = 0;//отчистка переменной результата от предыдущих вычислений
        xx = x[i];//подготовка значение, которое будет передано в вставку


        _asm {
            mov eax, xx
            cmp eax, 20
            jle LessEquals20
            mov ebx, eax//если х больше 20
            mov eax, 1200
            mov edx, 0
            idiv ebx
            movsx eax, ax
            mov y, eax
            neg y
            jmp End
            LessEquals20 ://если х меньше либо равно 20
            imul eax
            mov ecx, -3
            imul ecx
            mov y, eax
            End:
        }

        cout << " в Assembler ";
        if (x[i] <= 20) {
            
            cout << "y = -3 * x**2";
        }
        else {
            
            cout << "y = -1200 / x";
        }
        cout << " = " << y << endl<<endl;
    }


    system("pause"); 
    return 0;
}