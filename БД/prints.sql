PGDMP     .                    x            prints    12.2    12.2 _    ?           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ?           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            ?           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ?           1262    16572    prints    DATABASE     ?   CREATE DATABASE prints WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';
    DROP DATABASE prints;
                postgres    false            ?            1259    16623    accounts    TABLE     c   CREATE TABLE public.accounts (
    account character(20) NOT NULL,
    bank_id integer NOT NULL
);
    DROP TABLE public.accounts;
       public         heap    postgres    false            ?            1259    16609    banks    TABLE     _   CREATE TABLE public.banks (
    id bigint NOT NULL,
    bank character varying(30) NOT NULL
);
    DROP TABLE public.banks;
       public         heap    postgres    false            ?            1259    16607    banks_id_seq    SEQUENCE     u   CREATE SEQUENCE public.banks_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.banks_id_seq;
       public          postgres    false    211            ?           0    0    banks_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.banks_id_seq OWNED BY public.banks.id;
          public          postgres    false    210            ?            1259    16601    cities    TABLE     `   CREATE TABLE public.cities (
    id bigint NOT NULL,
    city character varying(30) NOT NULL
);
    DROP TABLE public.cities;
       public         heap    postgres    false            ?            1259    16599    cities_id_seq    SEQUENCE     v   CREATE SEQUENCE public.cities_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.cities_id_seq;
       public          postgres    false    209            ?           0    0    cities_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.cities_id_seq OWNED BY public.cities.id;
          public          postgres    false    208            ?            1259    16684 	   customers    TABLE     ?  CREATE TABLE public.customers (
    id bigint NOT NULL,
    first_name character varying(15) NOT NULL,
    second_name character varying(15) NOT NULL,
    third_name character varying(15) NOT NULL,
    city_id integer NOT NULL,
    address character varying(40) NOT NULL,
    birthday date NOT NULL,
    phone character(10),
    CONSTRAINT customers_birthday_check CHECK ((age((birthday)::timestamp with time zone, (CURRENT_DATE)::timestamp with time zone) >= '18 years'::interval))
);
    DROP TABLE public.customers;
       public         heap    postgres    false            ?            1259    16682    customers_id_seq    SEQUENCE     y   CREATE SEQUENCE public.customers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.customers_id_seq;
       public          postgres    false    222            ?           0    0    customers_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.customers_id_seq OWNED BY public.customers.id;
          public          postgres    false    221            ?            1259    16577 	   districts    TABLE     g   CREATE TABLE public.districts (
    id bigint NOT NULL,
    district character varying(15) NOT NULL
);
    DROP TABLE public.districts;
       public         heap    postgres    false            ?            1259    16575    districts_id_seq    SEQUENCE     y   CREATE SEQUENCE public.districts_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.districts_id_seq;
       public          postgres    false    203            ?           0    0    districts_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.districts_id_seq OWNED BY public.districts.id;
          public          postgres    false    202            ?            1259    16617    formats    TABLE     c   CREATE TABLE public.formats (
    id bigint NOT NULL,
    format character varying(20) NOT NULL
);
    DROP TABLE public.formats;
       public         heap    postgres    false            ?            1259    16615    formats_id_seq    SEQUENCE     w   CREATE SEQUENCE public.formats_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.formats_id_seq;
       public          postgres    false    213            ?           0    0    formats_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.formats_id_seq OWNED BY public.formats.id;
          public          postgres    false    212            ?            1259    16741    orders    TABLE     D  CREATE TABLE public.orders (
    id bigint NOT NULL,
    customer_id integer NOT NULL,
    producttype_id integer NOT NULL,
    publication character varying(20),
    picture bytea,
    worker_id integer NOT NULL,
    price integer NOT NULL,
    calculation integer NOT NULL,
    count integer,
    format_id integer NOT NULL,
    papertype_id integer NOT NULL,
    datastart date NOT NULL,
    dataplan date NOT NULL,
    datafact date NOT NULL,
    cost integer NOT NULL,
    comment text,
    account character(20),
    CONSTRAINT orders_calculation_check CHECK ((calculation >= 50)),
    CONSTRAINT orders_check CHECK ((dataplan > datastart)),
    CONSTRAINT orders_check1 CHECK (((datafact > datastart) AND (datafact < CURRENT_DATE))),
    CONSTRAINT orders_cost_check CHECK ((cost >= 0)),
    CONSTRAINT orders_count_check CHECK ((count > 0)),
    CONSTRAINT orders_datastart_check CHECK (((date_part('year'::text, datastart) >= (1920)::double precision) AND (date_part('year'::text, datastart) <= (2020)::double precision))),
    CONSTRAINT orders_price_check CHECK ((price >= 50))
);
    DROP TABLE public.orders;
       public         heap    postgres    false            ?            1259    16739    orders_id_seq    SEQUENCE     v   CREATE SEQUENCE public.orders_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.orders_id_seq;
       public          postgres    false    224            ?           0    0    orders_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.orders_id_seq OWNED BY public.orders.id;
          public          postgres    false    223            ?            1259    16637 
   papertypes    TABLE     ?   CREATE TABLE public.papertypes (
    id bigint NOT NULL,
    papertype character varying(30) NOT NULL,
    density integer NOT NULL,
    CONSTRAINT papertypes_density_check CHECK (((density > 39) AND (density < 351)))
);
    DROP TABLE public.papertypes;
       public         heap    postgres    false            ?            1259    16635    papertypes_id_seq    SEQUENCE     z   CREATE SEQUENCE public.papertypes_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.papertypes_id_seq;
       public          postgres    false    216            ?           0    0    papertypes_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.papertypes_id_seq OWNED BY public.papertypes.id;
          public          postgres    false    215            ?            1259    16646    prints    TABLE     V  CREATE TABLE public.prints (
    id bigint NOT NULL,
    print character varying(20) NOT NULL,
    type_id integer NOT NULL,
    district_id integer NOT NULL,
    address character varying(40) NOT NULL,
    phone character(10) NOT NULL,
    year integer NOT NULL,
    CONSTRAINT prints_year_check CHECK (((year > 1980) AND (year < 2021)))
);
    DROP TABLE public.prints;
       public         heap    postgres    false            ?            1259    16644    prints_id_seq    SEQUENCE     v   CREATE SEQUENCE public.prints_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.prints_id_seq;
       public          postgres    false    218            ?           0    0    prints_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.prints_id_seq OWNED BY public.prints.id;
          public          postgres    false    217            ?            1259    16593    producttypes    TABLE     m   CREATE TABLE public.producttypes (
    id bigint NOT NULL,
    producttype character varying(20) NOT NULL
);
     DROP TABLE public.producttypes;
       public         heap    postgres    false            ?            1259    16591    producttypes_id_seq    SEQUENCE     |   CREATE SEQUENCE public.producttypes_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.producttypes_id_seq;
       public          postgres    false    207            ?           0    0    producttypes_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.producttypes_id_seq OWNED BY public.producttypes.id;
          public          postgres    false    206            ?            1259    16585    types    TABLE     _   CREATE TABLE public.types (
    id bigint NOT NULL,
    type character varying(15) NOT NULL
);
    DROP TABLE public.types;
       public         heap    postgres    false            ?            1259    16583    types_id_seq    SEQUENCE     u   CREATE SEQUENCE public.types_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.types_id_seq;
       public          postgres    false    205            ?           0    0    types_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.types_id_seq OWNED BY public.types.id;
          public          postgres    false    204            ?            1259    16665    workers    TABLE     ?   CREATE TABLE public.workers (
    id bigint NOT NULL,
    first_name character varying(15) NOT NULL,
    second_name character varying(15) NOT NULL,
    third_name character varying(15) NOT NULL,
    print_id integer NOT NULL
);
    DROP TABLE public.workers;
       public         heap    postgres    false            ?            1259    16663    workers_id_seq    SEQUENCE     w   CREATE SEQUENCE public.workers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.workers_id_seq;
       public          postgres    false    220            ?           0    0    workers_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.workers_id_seq OWNED BY public.workers.id;
          public          postgres    false    219            ?
           2604    16612    banks id    DEFAULT     d   ALTER TABLE ONLY public.banks ALTER COLUMN id SET DEFAULT nextval('public.banks_id_seq'::regclass);
 7   ALTER TABLE public.banks ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    210    211    211            ?
           2604    16604 	   cities id    DEFAULT     f   ALTER TABLE ONLY public.cities ALTER COLUMN id SET DEFAULT nextval('public.cities_id_seq'::regclass);
 8   ALTER TABLE public.cities ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    208    209    209            ?
           2604    16687    customers id    DEFAULT     l   ALTER TABLE ONLY public.customers ALTER COLUMN id SET DEFAULT nextval('public.customers_id_seq'::regclass);
 ;   ALTER TABLE public.customers ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    222    221    222            ?
           2604    16580    districts id    DEFAULT     l   ALTER TABLE ONLY public.districts ALTER COLUMN id SET DEFAULT nextval('public.districts_id_seq'::regclass);
 ;   ALTER TABLE public.districts ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    202    203    203            ?
           2604    16620 
   formats id    DEFAULT     h   ALTER TABLE ONLY public.formats ALTER COLUMN id SET DEFAULT nextval('public.formats_id_seq'::regclass);
 9   ALTER TABLE public.formats ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    213    212    213            ?
           2604    16744 	   orders id    DEFAULT     f   ALTER TABLE ONLY public.orders ALTER COLUMN id SET DEFAULT nextval('public.orders_id_seq'::regclass);
 8   ALTER TABLE public.orders ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    224    223    224            ?
           2604    16640    papertypes id    DEFAULT     n   ALTER TABLE ONLY public.papertypes ALTER COLUMN id SET DEFAULT nextval('public.papertypes_id_seq'::regclass);
 <   ALTER TABLE public.papertypes ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    215    216    216            ?
           2604    16649 	   prints id    DEFAULT     f   ALTER TABLE ONLY public.prints ALTER COLUMN id SET DEFAULT nextval('public.prints_id_seq'::regclass);
 8   ALTER TABLE public.prints ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    217    218    218            ?
           2604    16596    producttypes id    DEFAULT     r   ALTER TABLE ONLY public.producttypes ALTER COLUMN id SET DEFAULT nextval('public.producttypes_id_seq'::regclass);
 >   ALTER TABLE public.producttypes ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    206    207    207            ?
           2604    16588    types id    DEFAULT     d   ALTER TABLE ONLY public.types ALTER COLUMN id SET DEFAULT nextval('public.types_id_seq'::regclass);
 7   ALTER TABLE public.types ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    205    204    205            ?
           2604    16668 
   workers id    DEFAULT     h   ALTER TABLE ONLY public.workers ALTER COLUMN id SET DEFAULT nextval('public.workers_id_seq'::regclass);
 9   ALTER TABLE public.workers ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    220    219    220            ?          0    16623    accounts 
   TABLE DATA           4   COPY public.accounts (account, bank_id) FROM stdin;
    public          postgres    false    214   ql                 0    16609    banks 
   TABLE DATA           )   COPY public.banks (id, bank) FROM stdin;
    public          postgres    false    211   ?l       }          0    16601    cities 
   TABLE DATA           *   COPY public.cities (id, city) FROM stdin;
    public          postgres    false    209   ?l       ?          0    16684 	   customers 
   TABLE DATA           o   COPY public.customers (id, first_name, second_name, third_name, city_id, address, birthday, phone) FROM stdin;
    public          postgres    false    222   ?l       w          0    16577 	   districts 
   TABLE DATA           1   COPY public.districts (id, district) FROM stdin;
    public          postgres    false    203   ?l       ?          0    16617    formats 
   TABLE DATA           -   COPY public.formats (id, format) FROM stdin;
    public          postgres    false    213   mm       ?          0    16741    orders 
   TABLE DATA           ?   COPY public.orders (id, customer_id, producttype_id, publication, picture, worker_id, price, calculation, count, format_id, papertype_id, datastart, dataplan, datafact, cost, comment, account) FROM stdin;
    public          postgres    false    224   ?m       ?          0    16637 
   papertypes 
   TABLE DATA           <   COPY public.papertypes (id, papertype, density) FROM stdin;
    public          postgres    false    216   ?m       ?          0    16646    prints 
   TABLE DATA           W   COPY public.prints (id, print, type_id, district_id, address, phone, year) FROM stdin;
    public          postgres    false    218   ?m       {          0    16593    producttypes 
   TABLE DATA           7   COPY public.producttypes (id, producttype) FROM stdin;
    public          postgres    false    207   ?m       y          0    16585    types 
   TABLE DATA           )   COPY public.types (id, type) FROM stdin;
    public          postgres    false    205   fn       ?          0    16665    workers 
   TABLE DATA           T   COPY public.workers (id, first_name, second_name, third_name, print_id) FROM stdin;
    public          postgres    false    220   ?n       ?           0    0    banks_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.banks_id_seq', 1, false);
          public          postgres    false    210            ?           0    0    cities_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.cities_id_seq', 1, false);
          public          postgres    false    208            ?           0    0    customers_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.customers_id_seq', 1, false);
          public          postgres    false    221            ?           0    0    districts_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.districts_id_seq', 8, true);
          public          postgres    false    202            ?           0    0    formats_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.formats_id_seq', 1, false);
          public          postgres    false    212            ?           0    0    orders_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.orders_id_seq', 1, false);
          public          postgres    false    223            ?           0    0    papertypes_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.papertypes_id_seq', 1, false);
          public          postgres    false    215            ?           0    0    prints_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.prints_id_seq', 1, false);
          public          postgres    false    217            ?           0    0    producttypes_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.producttypes_id_seq', 13, true);
          public          postgres    false    206            ?           0    0    types_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.types_id_seq', 1, false);
          public          postgres    false    204            ?           0    0    workers_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.workers_id_seq', 1, false);
          public          postgres    false    219            ?
           2606    16627    accounts accounts_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.accounts
    ADD CONSTRAINT accounts_pkey PRIMARY KEY (account);
 @   ALTER TABLE ONLY public.accounts DROP CONSTRAINT accounts_pkey;
       public            postgres    false    214            ?
           2606    16614    banks banks_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.banks
    ADD CONSTRAINT banks_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.banks DROP CONSTRAINT banks_pkey;
       public            postgres    false    211            ?
           2606    16606    cities cities_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.cities
    ADD CONSTRAINT cities_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.cities DROP CONSTRAINT cities_pkey;
       public            postgres    false    209            ?
           2606    16690    customers customers_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    222            ?
           2606    16582    districts districts_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.districts
    ADD CONSTRAINT districts_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.districts DROP CONSTRAINT districts_pkey;
       public            postgres    false    203            ?
           2606    16622    formats formats_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.formats
    ADD CONSTRAINT formats_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.formats DROP CONSTRAINT formats_pkey;
       public            postgres    false    213            ?
           2606    16756    orders orders_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_pkey;
       public            postgres    false    224            ?
           2606    16643    papertypes papertypes_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.papertypes
    ADD CONSTRAINT papertypes_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.papertypes DROP CONSTRAINT papertypes_pkey;
       public            postgres    false    216            ?
           2606    16652    prints prints_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.prints
    ADD CONSTRAINT prints_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.prints DROP CONSTRAINT prints_pkey;
       public            postgres    false    218            ?
           2606    16598    producttypes producttypes_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.producttypes
    ADD CONSTRAINT producttypes_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.producttypes DROP CONSTRAINT producttypes_pkey;
       public            postgres    false    207            ?
           2606    16590    types types_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.types
    ADD CONSTRAINT types_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.types DROP CONSTRAINT types_pkey;
       public            postgres    false    205            ?
           2606    16670    workers workers_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.workers
    ADD CONSTRAINT workers_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.workers DROP CONSTRAINT workers_pkey;
       public            postgres    false    220            ?
           2606    16628    accounts accounts_bank_id_fkey    FK CONSTRAINT     }   ALTER TABLE ONLY public.accounts
    ADD CONSTRAINT accounts_bank_id_fkey FOREIGN KEY (bank_id) REFERENCES public.banks(id);
 H   ALTER TABLE ONLY public.accounts DROP CONSTRAINT accounts_bank_id_fkey;
       public          postgres    false    2782    214    211            ?
           2606    16691     customers customers_city_id_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_city_id_fkey FOREIGN KEY (city_id) REFERENCES public.cities(id);
 J   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_city_id_fkey;
       public          postgres    false    222    209    2780            ?
           2606    16782    orders orders_account_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_account_fkey FOREIGN KEY (account) REFERENCES public.accounts(account);
 D   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_account_fkey;
       public          postgres    false    214    2786    224            ?
           2606    16757    orders orders_customer_id_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(id);
 H   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_customer_id_fkey;
       public          postgres    false    224    2794    222            ?
           2606    16772    orders orders_format_id_fkey    FK CONSTRAINT        ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_format_id_fkey FOREIGN KEY (format_id) REFERENCES public.formats(id);
 F   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_format_id_fkey;
       public          postgres    false    2784    213    224            ?
           2606    16777    orders orders_papertype_id_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_papertype_id_fkey FOREIGN KEY (papertype_id) REFERENCES public.papertypes(id);
 I   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_papertype_id_fkey;
       public          postgres    false    224    2788    216            ?
           2606    16762 !   orders orders_producttype_id_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_producttype_id_fkey FOREIGN KEY (producttype_id) REFERENCES public.producttypes(id);
 K   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_producttype_id_fkey;
       public          postgres    false    207    224    2778            ?
           2606    16767    orders orders_worker_id_fkey    FK CONSTRAINT        ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_worker_id_fkey FOREIGN KEY (worker_id) REFERENCES public.workers(id);
 F   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_worker_id_fkey;
       public          postgres    false    2792    224    220            ?
           2606    16658    prints prints_district_id_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY public.prints
    ADD CONSTRAINT prints_district_id_fkey FOREIGN KEY (district_id) REFERENCES public.districts(id);
 H   ALTER TABLE ONLY public.prints DROP CONSTRAINT prints_district_id_fkey;
       public          postgres    false    2774    218    203            ?
           2606    16653    prints prints_type_id_fkey    FK CONSTRAINT     y   ALTER TABLE ONLY public.prints
    ADD CONSTRAINT prints_type_id_fkey FOREIGN KEY (type_id) REFERENCES public.types(id);
 D   ALTER TABLE ONLY public.prints DROP CONSTRAINT prints_type_id_fkey;
       public          postgres    false    205    2776    218            ?
           2606    16671    workers workers_print_id_fkey    FK CONSTRAINT     ~   ALTER TABLE ONLY public.workers
    ADD CONSTRAINT workers_print_id_fkey FOREIGN KEY (print_id) REFERENCES public.prints(id);
 G   ALTER TABLE ONLY public.workers DROP CONSTRAINT workers_print_id_fkey;
       public          postgres    false    2790    218    220            ?      x?????? ? ?            x?????? ? ?      }      x?????? ? ?      ?      x?????? ? ?      w   x   x?]?;
?@C????=????VZ?x??272k!?E`??$?B?&ɱ?ڴ??⒌?h?,?+4Rb????)?^	??ς???M?l???=+?O??"?^??W?:Z????      ?      x?????? ? ?      ?      x?????? ? ?      ?      x?????? ? ?      ?      x?????? ? ?      {   u   x???	?@D?w??!1ZM?1*??:̝??0?Q??X????}V?q??F?E???C!???=`?חX????NK!٩{???2?T?A??5Z?C2\F?qj?>56?;W???mUV      y      x?????? ? ?      ?      x?????? ? ?     