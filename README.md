# Úvod

Cílem [práce] (https://www.fit.vut.cz/study/thesis/24108/.cs?year=2020&stud=Vacul%C3%ADk) bylo navrhnout aplikace, které demonstrují sílu strojového učení pro tvorbu umělé inteligence ve videohrách. K řešení této problematiky je použita sada nástrojů ML-Agents, která umožňuje tvorbu inteligentních agentů v enginu Unity. Jednotlivé demonstrační aplikace jsou zaměřeny na různé scénáře využití této sady. Pro trénování je použito zpětnovazební a imitační učení.

# Aplikace

[1. Fighter](https://github.com/vaculikjan/IBT-fighter)

# Návod na spuštění

Pro spuštění jednotlivých aplikací s již vytrénovnaou neuronovou sítí, je nutné mít nainstalované *Unity* (funknčonst zajištěna na verzi **2019.4.17f1**). 

Samotné spuštění aplikace se provádí spuštěním souboru scény:

```
projects
│  
│ 
└───[application]
    │   
    │
    └───Assets
        │   
        │   
        └───Scenes
            │   
            │   
            └───Scenes[scene].unity
```
kde ```[applicaton]``` je název aplikace (Fighter, Racer, Nightmares) a ```[scene]``` název scény. Projekt se z této scény už spustí tlačítkem *Play*.

Pro spuštění trénování je nutné:

1. Nainstalovat *Unity* (funknčonst zajištěna na verzi **2019.4.17f1**)
2. Nainstalovat *Python* (pro projekt byla použita verze 3.8)
3. Nainstalovat balíček pro trénování příkazem: ```$pip install mlagents```

Pro instalaci Python balíčku je doporučené použít virtuální prostředí.

Trénování se spouští z příkazové řádky z adresáře ```projects``` příkazem: ```$mlagents-learn .\[application]\config\[config_file].yaml --run-id=[nazev_behu]```, kde ```[application]``` je název aplikace (Fighter, Racer, Nightmares), ```[config_file]``` je název konfiguračního souboru a ```[nazev_behu]``` je libovolný název, podle ktrého bude konkrétní běh identifikován. Pokud vše proběhlo bez problémů, vykreslí se *Unity* logo a uživatel je vybídnut ke spuštění aplikace, kterou chce uživatel trénovat (viz předchozí sekce). 

Výsledek tohoto běhu se uloží do adresáře ze kterého byl příkaz spuštěn. Na další přepínače je možné se podívat příkazem: ```$mlagents-learn --help```.

Na výsledky je možné se podívat z adresáře, ve kterém je uložen adresář ```results```, příkazem ```$tensorboard --logdir results --port 6006```. Poté se v okně prohlížeče zadá: http://localhost:6006/

Pro aplikaci Nightmares jsou přítomny dvě verze. Nightmares je verze, ve které jsou přítomy záznamy všech běhů a jejíž ovládání bylo popsáno v technické zprávě. Nightmares_best obsahuje nastavení, se kterým bylo dosaženo nejlepšího výsledku a jehož agent byl zachycen na videu.

