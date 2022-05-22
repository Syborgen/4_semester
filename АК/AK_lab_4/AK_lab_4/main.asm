.386
.model flat, stdcall
option casemap : none
.data
DivZ	dd  0
N		dd	5
k		dd	1
A		dd	8,65,19,11,12,16
M		dd	1,4,1,2,1,3,1
Z		dd	5 dup(?)
.code
;������� k*M[k]
imulEBX PROC
	imul ebx,k				;ebx->k*M[k]
	cdq						;��������� ���� eax � edx
	ret
imulEBX ENDP
main PROC
	mov  ecx, N
forN:
	mov	 esi, k	;k-������ ��������
	mov	 eax, A[esi*4];eax->A[k]
	doneg:             ;
	neg eax         ;eax=|A[k]|
	js doneg              ;
    mov ebx, A[ecx*4]     ;ebx->A[N+1-k]
	cmp  ebx, 0	;A[N+1-k] ?= 0
	je   ZDiv	;A[N+1-k] == 0 -> ZDiv
	cdq		;��������� ���� eax � edx
	idiv ebx	;eax-> |A[k]|/A[N+1-k]
	mov ebx, M[ecx*4+4]   ;ebx->M[N+2-k]
	cmp  ebx, 0	;M[N+2-k] ?= 0
	je   ZDiv	;M[N+2-k] == 0 -> ZDiv
	cdq	;��������� ���� eax � edx
	idiv ebx	;eax-> |A[k]|/A[N+1-k]/M[N+2-k]
	mov ebx,M[esi*4]   ;ebx->M[k]
	call imulEBX
	sub ebx,eax            ;ebx->k*M[k] - |A[k]|/A[N+1-k]/M[N+2-k]
	mov Z[esi*4],ebx        ;z[k] = k*M[k] - |A[k]|/A[N+1-k]/M[N+2-k]
	push ebx          ;z[k] � ����
	inc k		;k+1
	loop forN	;����
	pop edi		;z[5]
	pop edx		;z[4]
	pop ecx		;z[3]
	pop ebx		;z[2]
	pop eax		;z[1]

	ret
ZDiv:					;������� �� 0
	mov	DivZ, 1				;DivZ -> 1 ���� ������� �� 0
	ret
main ENDP
END main
