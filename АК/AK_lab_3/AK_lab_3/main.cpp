#include <iostream>
#include <cstdlib> // ��� system
using namespace std;

int main()
{
    setlocale(LC_ALL, "Russian");
    int x[6] = {-3,6,8,20,25,43};
    int xx;//���������� ��� �������� �������� � ������������ �������
    int y;
    
    for (int i = 0; i < 6; i++) {
        cout << "Test �" << i+1 << ": x = " << x[i] << " ---------------------------------------" << endl;
        
        cout << " � C++ ";
        if (x[i] <= 20) {
            y = -3 * pow(x[i], 2);
            cout << "y = -3 * x**2";
        }
        else {
            y = -1200 / x[i];//������������� �������
            cout << "y = -1200 / x";
        }
        cout << " = " << y << endl;
        y = 0;//�������� ���������� ���������� �� ���������� ����������
        xx = x[i];//���������� ��������, ������� ����� �������� � �������


        _asm {
            mov eax, xx
            cmp eax, 20
            jle LessEquals20
            mov ebx, eax//���� � ������ 20
            mov eax, 1200
            mov edx, 0
            idiv ebx
            movsx eax, ax
            mov y, eax
            neg y
            jmp End
            LessEquals20 ://���� � ������ ���� ����� 20
            imul eax
            mov ecx, -3
            imul ecx
            mov y, eax
            End:
        }

        cout << " � Assembler ";
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