select * from prints limit 3

select id, customer_id as индекс_заказчика, (select producttype from producttypes where id=producttype_id) as изделие,
publication as название_издания,picture as титульная_страница,
calculation as тираж, count as количество_листов, (select format from formats where id=format_id) as формат, 
(select papertype from papertypes where id = papertype_id) as вид_бумаги, datastart as дата_принятия, 
dataplan as дата_выполнения_план, datafact as дата_выполнения_факт, cost as предоплата, comment as доп_сведения
,account as банковский_номер from orders 

--типографии, в которых есть работник с именем ...
select
p.print as типография,
count(w.first_name) as количество_работников_с_именем 
from prints as p, 
workers as w 
where p.id=w.print_id and 
w.first_name='Иван'
group by p.print
order by 1

--заказчики, которые живут в городе ...
select cust.first_name as имя,
cust.second_name as фамилия,
cust.third_name as отчество,
city.city as город
from customers as cust,
cities as city
where cust.city_id=city.id and
city.city='Донецк'
order by 4

--заказчики, родившиеся в году ..., который совпадает с годом основания типографии
select
cust.first_name as имя,
cust.second_name as фамилия,
cust.third_name as отчество,
extract(year from cust.birthday) as год_рождения, 
p.year as год_основаия, 
p.print as типография
from customers as cust,
orders as o,
workers as w,
prints as p
where cust.id = o.customer_id and 
o.worker_id=w.id and 
w.print_id=p.id and 
extract(year from cust.birthday)=p.year and 
p.year = 1999
order by p.print


--заказы принятые в указанный год, год принятия которых на 20 лет больше чем год основания типографии
select
o.publication as название,
p.year as год_основания, 
extract(year from o.datastart) as год_принятия
from workers as w,
prints as p,
orders as o
where p.id=w.print_id and
o.worker_id=w.id and
(extract(year from o.datastart)-20)=p.year and
extract(year from o.datastart)=2015
order by p.year 



--Работники с типографиями в которых они работают и районом расположения типографии
select 
w.first_name as имя,
w.second_name as фамилия,
w.third_name as отчество,
p.print as типография ,
d.district as район
from workers as w,
prints as p,
districts as d
where p.id=w.print_id and 
p.district_id=d.id
order by 1,2,3

--заказчики с городами проживания и количеством заказов
select cust.id,
cust.first_name as имя
,cust.second_name as фамилия,
cust.third_name as отчество,
c.city as город,
count(o.*) as количество_заказов
from customers as cust,
cities as c,
orders as o
where cust.city_id=c.id and
o.customer_id=cust.id
group by 1,2,3,4,5
order by 6 desc

--банковские номера с банком и количеством заказов оплаченных ими
select
a.account as банковский_счет,
b.bank as банк,
count(o.*) as количество_заказов
from accounts as a,
banks as b,
orders as o
where a.account=o.account and 
a.bank_id = b.id
group by 1,2
order by 3 desc



--номера бановских счетов и заказы которые были оплачены ими (левое)
select
a.account as банковский_счет,
o.id as номер_заказа
from accounts as a
left join orders as o on o.account=a.account
order by 2 desc

--все города и заказчики в городах
select c.id,
c.city as Город,
cust.first_name as имя,
cust.second_name as фамилия,
cust.third_name as отчество
from customers as cust 
right join cities as c on c.id=cust.city_id
order by 1 desc




--информация о банковских счетах, которые включают в себя последовательность цифр 1234
select aa.account,
b.bank
from (select account,bank_id from accounts where account like '%1234%') as aa
left join banks as b on aa.bank_id=b.id

select price,calculation,cost,(price * calculation) from orders 

--КОНЕЦ ПРОСТЫХ ЗАПРОСОВ
--начало запросов с агрегатными функциями

--вывести итоговую стоимость заказов заказчиков
select cust.id,
cust.first_name as имя,
cust.second_name as фамилия,
cust.third_name as отчество,
sum(o.cost*o.calculation) as денег_потрачено
from customers as cust,
orders as o
where o.customer_id=cust.id
group by 1,2,3,4
order by 5

--определить количество работников, задействованных в типографиях данного района
select d.district as район,
count (w.*) as работников
from prints as p,
workers as w,
districts as d
where d.id=p.district_id and
p.id=w.print_id
group by 1
order by 2

--вывести информацию о типографиях, количество заказов в которых больше указанного
select p.id,
p.print,
count(o.*)
from prints as p,
workers as w,
orders as o
where p.id=w.print_id and
o.worker_id=w.id
group by 1,2
having(count(o.*)>50)
order by 3

--вывести информацию о банковских счетах, потрачено денег на которых больше указанного числа
select a.account as банковский_счет,
b.bank as банк,
sum(o.cost*o.calculation) as денег_потрачено
from accounts as a,
banks as b,
orders as o
where o.account=a.account and
a.bank_id = b.id and
b.bank='Пумб'
group by 1,2
having (sum(o.cost*o.calculation)>100000000)
order by 3


--определить количество работников и количество заказов типографий
select p.id,
p.print as типография,
count(w.*) as количество_работников,
sum(sel.c) as количество_заказов
from prints as p,
workers as w,
(select wo.id as id,count(o.*) as c
from workers as wo,
orders as o
where wo.id=o.worker_id
group by 1) as sel
where p.id=w.print_id and
sel.id=w.id
group by 1,2
order by 4 desc,3 

select extract(year from current_date)
--определить количество работников, которые работают в типографиях, существующих более 30 лет
select p.id,
p.print,
count (w.*),
p.возраст,
p.год_основания
from workers as w,
(select id,print,year as год_основания,(extract(year from current_date)-year) as возраст from prints where (extract(year from current_date)-year>=30)) as p
where p.id=w.print_id
group by 1,2,4,5
order by 4 desc,3






