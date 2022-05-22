.386
.model flat, stdcall
option casemap :none
include \masm32\include\masm32.inc
include \masm32\include\kernel32.inc
include \masm32\macros\macros.asm
includelib \masm32\lib\masm32.lib
includelib \masm32\lib\kernel32.lib

.data

a db 18h, 23h
b db 0h, 52h
x db 37, 23
y db 5
ad db 0h, 0h
de db 0h, 0h
del db 0h, 0h
.code
start:
;сложение
mov al, b+1 ;
add al, a+1 ;
daa ;
mov ad+1, al
mov al, b ;
adc al, a ;
daa ;
mov ad, al
;сложение

;вычитание
mov al, a+1 ;
sub al, b+1 ;
das ;
mov de+1, al
mov al, a ;
sbb al, b ;
daa ;
mov de, al
;вычитание

;деление
xor ah, ah
mov al, x ;
aad ;
div y ;
mov del+0, al
mov al, x+1 ;
aad ;
div y ;
mov del+1, al
;деление

exit
end start
