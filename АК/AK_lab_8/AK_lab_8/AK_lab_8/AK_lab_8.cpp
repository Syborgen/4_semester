#include <iostream>
#include <time.h>
#include <chrono>
using namespace std;

int main()
{
    srand(time(0));
    const int len = 4096;
    float Bmax = 0,  del = 3.0, count = 1024.0;
    float img[len];
    for (int i = 0; i < len; i++)
        if ((i-2) %4  == 0) {
            img[i] = (float)(rand() % 256);
        }
        else {
            img[i] = (float)(rand() % 100);
        }
    auto begin = std::chrono::steady_clock::now();
    _asm
    {
        mov     ecx, len
        shr     ecx, 2
        xor esi, esi; смещение в массиве
        fori : ; цикл
        movups  xmm0, img[esi]; xmm0 = r, g, b, a
        maxps   xmm3, xmm0; max R, G, B, A компонент
        add     esi, 16; следующий пиксель
        loop    fori; цикл
        movups  xmm0, xmm3; xmm0 = max R, G, B, A компонент
        shufps  xmm0, xmm0, 10101010b; xmm0 = max B компонент
        movss   Bmax, xmm0; Bmax = max B компонент
    }
    auto end = std::chrono::steady_clock::now();
    auto elapsed_ms = std::chrono::duration_cast<std::chrono::nanoseconds>(end - begin);

    cout << "The time ASM: " << elapsed_ms.count() << " nanoseconds\n";
    cout << "B-max: " << Bmax << endl;

    begin = std::chrono::steady_clock::now();

    Bmax = img[2];

    for (int i = 0; i < len; i ++)
    {
        if((i - 2) % 4 == 0)
        if (Bmax < img[i])
            Bmax = img[i];
    }

    end = std::chrono::steady_clock::now();
    elapsed_ms = std::chrono::duration_cast<std::chrono::nanoseconds>(end - begin);

    cout << "\nThe time C++: " << elapsed_ms.count() << " nanoseconds\n";
    cout << "B-max: " << Bmax << endl;
}
