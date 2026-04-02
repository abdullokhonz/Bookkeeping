using Bookkeeping.Contracts.Enums;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookkeeping.Services.Seeders
{
    // Теперь модель стала максимально простой: ID категории, Код счета, Название
    public record AccountSeedModel(Guid CategoryId, string Code, string Name);

    public class ChartOfAccountsSeeder
    {
        private readonly PostgreSQLDbContext _context;
        private readonly ILogger<ChartOfAccountsSeeder> _logger;

        public ChartOfAccountsSeeder(PostgreSQLDbContext context, ILogger<ChartOfAccountsSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> SeedAsync(CancellationToken ct = default)
        {
            // 1. Заранее парсим твои готовые ID категорий из БД
            var oborotnieId = Guid.Parse("ae24f717-a65c-49eb-bc1d-a5670432a775");
            var vneoborotnieId = Guid.Parse("67966a47-1c02-4e93-9d00-8ac2edbb3011");
            var tekushieObyazId = Guid.Parse("b1b939be-5123-46e6-8697-9db6d51d0107");
            var dolgoObyazId = Guid.Parse("1c534194-3d07-4922-bb1c-8527f7643715");
            var kapitalId = Guid.Parse("3ff6129b-9a9a-422f-8cfb-178555dbbb54");
            var operDohodiId = Guid.Parse("80f231a0-0ba1-43ba-ae84-ba81b4a20bfb");
            var operRashodiId = Guid.Parse("9a23a5a9-de17-45fa-bcb3-223c4eb6d507");
            var neoperDohodiRashodiId = Guid.Parse("cb42f5cc-5298-4e08-aaa0-f7b73e3e4d3a");

            // Вспомогательная функция для добавления "00" в конец кода
            string FullCode(string shortCode) => shortCode + "00";

            // 2. Формируем список счетов с привязкой к конкретным ID
            var accountsToSeed = new List<AccountSeedModel>
            {
                // ========== ОБОРОТНЫЕ АКТИВЫ ==========
                new(oborotnieId, FullCode("10100"), "Денежные средства в кассе"),
                new(oborotnieId, FullCode("10110"), "Денежные средства в национальной валюте"),
                new(oborotnieId, FullCode("10120"), "Денежные средства в иностранной валюте"),
                new(oborotnieId, FullCode("10130"), "Денежные документы"),
                new(oborotnieId, FullCode("10140"), "Денежные эквиваленты"),
                new(oborotnieId, FullCode("10200"), "Денежные средства в банке"),
                new(oborotnieId, FullCode("10210"), "Счета в национальной валюте"),
                new(oborotnieId, FullCode("10220"), "Счета в иностранной валюте в местных банках"),
                new(oborotnieId, FullCode("10230"), "Счета в зарубежных банках"),
                new(oborotnieId, FullCode("10240"), "Денежные средства в банках, ограниченных к использованию"),
                new(oborotnieId, FullCode("10250"), "Денежные средства в пути"),
                new(oborotnieId, FullCode("10300"), "Краткосрочные инвестиции"),
                new(oborotnieId, FullCode("10310"), "Долговые ценные бумаги"),
                new(oborotnieId, FullCode("10320"), "Долевые ценные бумаги"),
                new(oborotnieId, FullCode("10330"), "Займы выданные"),
                new(oborotnieId, FullCode("10340"), "Депозитные вклады"),
                new(oborotnieId, FullCode("10350"), "Текущая часть долгосрочных инвестиций"),
                new(oborotnieId, FullCode("10360"), "Прочие краткосрочные инвестиции"),
                new(oborotnieId, FullCode("10400"), "Торговая дебиторская задолженность"),
                new(oborotnieId, FullCode("10410"), "Счета к получению"),
                new(oborotnieId, FullCode("10420"), "Векселя к получению"),
                new(oborotnieId, FullCode("10430"), "Резервы по сомнительным долгам"),
                new(oborotnieId, FullCode("10500"), "Прочая дебиторская задолженность"),
                new(oborotnieId, FullCode("10510"), "Авансы, выданные поставщикам"),
                new(oborotnieId, FullCode("10520"), "Дебиторская задолженность персонала"),
                new(oborotnieId, FullCode("10530"), "Налоги, оплаченные авансом"),
                new(oborotnieId, FullCode("10540"), "Налоги, подлежащие возмещению"),
                new(oborotnieId, FullCode("10550"), "Проценты к получению"),
                new(oborotnieId, FullCode("10560"), "Дивиденды к получению"),
                new(oborotnieId, FullCode("10570"), "Текущая часть долгосрочной дебиторской задолженности"),
                new(oborotnieId, FullCode("10580"), "Дебиторская задолженность дочерних (материнских) предприятий"),
                new(oborotnieId, FullCode("10590"), "Дебиторская задолженность по прочим операциям"),
                new(oborotnieId, FullCode("10600"), "Задолженность учредителей (участников) по вкладам в уставный капитал"),
                new(oborotnieId, FullCode("10700"), "Товарно-материальные запасы"),
                new(oborotnieId, FullCode("10710"), "Товары"),
                new(oborotnieId, FullCode("10719"), "Нереализованная торговая наценка"),
                new(oborotnieId, FullCode("10720"), "Сырье и материалы"),
                new(oborotnieId, FullCode("10730"), "Незавершенное производство"),
                new(oborotnieId, FullCode("10740"), "Готовая продукция"),
                new(oborotnieId, FullCode("10750"), "Сельхозпродукция с биологических активов"),
                new(oborotnieId, FullCode("10760"), "Топливо"),
                new(oborotnieId, FullCode("10770"), "Запасные части"),
                new(oborotnieId, FullCode("10780"), "Инвентарь и принадлежности"),
                new(oborotnieId, FullCode("10790"), "Прочие запасы"),
                new(oborotnieId, FullCode("10800"), "Расходы, оплаченные авансом"),
                new(oborotnieId, FullCode("10810"), "Услуги, оплаченные авансом"),
                new(oborotnieId, FullCode("10820"), "Аренда, оплаченная авансом"),
                new(oborotnieId, FullCode("10830"), "Прочие авансированные платежи"),
                new(oborotnieId, FullCode("10900"), "Внеоборотные активы для продажи"),
                new(oborotnieId, FullCode("10910"), "Основные средства, предназначенные для продажи"),
                new(oborotnieId, FullCode("10920"), "Краткосрочные активы прекращенной деятельности"),
                new(oborotnieId, FullCode("10930"), "Прочие внеоборотные активы для продажи"),
                
                // ========== ВНЕОБОРОТНЫЕ АКТИВЫ ==========
                new(vneoborotnieId, FullCode("11000"), "Основные средства"),
                new(vneoborotnieId, FullCode("11010"), "Здания и сооружения"),
                new(vneoborotnieId, FullCode("11020"), "Машины и оборудование"),
                new(vneoborotnieId, FullCode("11030"), "Конторское оборудование"),
                new(vneoborotnieId, FullCode("11040"), "Мебель и принадлежности"),
                new(vneoborotnieId, FullCode("11050"), "Транспортные средства"),
                new(vneoborotnieId, FullCode("11060"), "Благоустройство арендованной собственности"),
                new(vneoborotnieId, FullCode("11070"), "Благоустройство земельных участков"),
                new(vneoborotnieId, FullCode("11080"), "Прочие основные средства"),
                new(vneoborotnieId, FullCode("11090"), "Незавершенное строительство"),
                new(vneoborotnieId, FullCode("11100"), "Накопленный износ основных средств"),
                new(vneoborotnieId, FullCode("11110"), "Накопленный износ - здания и сооружения"),
                new(vneoborotnieId, FullCode("11120"), "Накопленный износ - машины и оборудования"),
                new(vneoborotnieId, FullCode("11130"), "Накопленный износ - конторское оборудование"),
                new(vneoborotnieId, FullCode("11140"), "Накопленный износ - мебель и принадлежности"),
                new(vneoborotnieId, FullCode("11150"), "Накопленный износ - транспортные средства"),
                new(vneoborotnieId, FullCode("11160"), "Накопленный износ - благоустройство арендованной собственности"),
                new(vneoborotnieId, FullCode("11170"), "Накопленный износ - благоустройство земельных участков"),
                new(vneoborotnieId, FullCode("11180"), "Накопленный износ - прочие основные средства"),
                new(vneoborotnieId, FullCode("11200"), "Природные ресурсы"),
                new(vneoborotnieId, FullCode("11210"), "Месторождения минеральных руд"),
                new(vneoborotnieId, FullCode("11220"), "Месторождения углеводородов"),
                new(vneoborotnieId, FullCode("11290"), "Накопленное истощение природных ресурсов"),
                new(vneoborotnieId, FullCode("11300"), "Нематериальные активы"),
                new(vneoborotnieId, FullCode("11310"), "Право пользования землей"),
                new(vneoborotnieId, FullCode("11320"), "Гудвилл"),
                new(vneoborotnieId, FullCode("11330"), "Патенты, лицензии, франшизы"),
                new(vneoborotnieId, FullCode("11340"), "Торговые марки"),
                new(vneoborotnieId, FullCode("11350"), "Авторские права"),
                new(vneoborotnieId, FullCode("11360"), "Программные обеспечения"),
                new(vneoborotnieId, FullCode("11370"), "Прочие нематериальные активы"),
                new(vneoborotnieId, FullCode("11390"), "Накопленная амортизация нематериальных активов"),
                new(vneoborotnieId, FullCode("11400"), "Биологические активы"),
                new(vneoborotnieId, FullCode("11410"), "Животные (потребляемые биологические активы)"),
                new(vneoborotnieId, FullCode("11420"), "Животные (плодоносящие биологические активы)"),
                new(vneoborotnieId, FullCode("11430"), "Растения (потребляемые биологические активы)"),
                new(vneoborotnieId, FullCode("11440"), "Плодоносящие растения"),
                new(vneoborotnieId, FullCode("11450"), "Биологические активы, учитываемые по фактическим затратам"),
                new(vneoborotnieId, FullCode("11460"), "Другие биологические активы"),
                new(vneoborotnieId, FullCode("11500"), "Инвестиции в недвижимость"),
                new(vneoborotnieId, FullCode("11510"), "Здания и сооружения"),
                new(vneoborotnieId, FullCode("11520"), "Реконструкция объектов инвестиции в недвижимость"),
                new(vneoborotnieId, FullCode("11600"), "Долгосрочные инвестиции"),
                new(vneoborotnieId, FullCode("11610"), "Долговые ценные бумаги"),
                new(vneoborotnieId, FullCode("11620"), "Займы, выданные"),
                new(vneoborotnieId, FullCode("11630"), "Инвестиции в дочерние предприятия"),
                new(vneoborotnieId, FullCode("11640"), "Инвестиции в совместную деятельность"),
                new(vneoborotnieId, FullCode("11650"), "Инвестиции в ассоциированные предприятия"),
                new(vneoborotnieId, FullCode("11660"), "Прочие долгосрочные инвестиции"),
                new(vneoborotnieId, FullCode("11661"), "Дисконты (cкидки) по долгосрочным инвестициям"),
                new(vneoborotnieId, FullCode("11662"), "Премии (надбавки) по долгосрочным инвестициям"),
                new(vneoborotnieId, FullCode("11700"), "Отсроченные налоговые требования"),
                new(vneoborotnieId, FullCode("11800"), "Долгосрочная дебиторская задолженность"),
                new(vneoborotnieId, FullCode("11810"), "Долгосрочная дебиторская задолженность покупателей и заказчиков"),
                new(vneoborotnieId, FullCode("11820"), "Векселя полученные"),
                new(vneoborotnieId, FullCode("11830"), "Долгосрочные отсроченные расходы"),
                new(vneoborotnieId, FullCode("11840"), "Прочая долгосрочная дебиторская задолженность"),
                new(vneoborotnieId, FullCode("11900"), "Долгосрочные активы прекращенной деятельности"),

                // ========== ТЕКУЩИЕ ОБЯЗАТЕЛЬСТВА ==========
                new(tekushieObyazId, FullCode("22000"), "Торговая кредиторская задолженность"),
                new(tekushieObyazId, FullCode("22010"), "Счета к оплате"),
                new(tekushieObyazId, FullCode("22020"), "Краткосрочные векселя к оплате"),
                new(tekushieObyazId, FullCode("22030"), "Авансы полученные"),
                new(tekushieObyazId, FullCode("22040"), "Прочие счета к оплате"),
                new(tekushieObyazId, FullCode("22100"), "Краткосрочные долговые обязательства"),
                new(tekushieObyazId, FullCode("22110"), "Банковские кредиты, займы"),
                new(tekushieObyazId, FullCode("22120"), "Прочие кредиты, займы"),
                new(tekushieObyazId, FullCode("22130"), "Текущая часть долгосрочных долговых обязательств"),
                new(tekushieObyazId, FullCode("22140"), "Прочие краткосрочные долговые обязательства"),
                new(tekushieObyazId, FullCode("22141"), "Дисконты (скидки) по облигациям и векселям"),
                new(tekushieObyazId, FullCode("22142"), "Премии (надбавки) по облигациям"),
                new(tekushieObyazId, FullCode("22200"), "Краткосрочные начисленные обязательства"),
                new(tekushieObyazId, FullCode("22210"), "Зарплата к выплате"),
                new(tekushieObyazId, FullCode("22220"), "Пенсионный налог к выплате"),
                new(tekushieObyazId, FullCode("22230"), "Подоходный налог к выплате"),
                new(tekushieObyazId, FullCode("22240"), "Социальный налог к выплате"),
                new(tekushieObyazId, FullCode("22250"), "Дивиденды к выплате"),
                new(tekushieObyazId, FullCode("22260"), "Проценты к оплате"),
                new(tekushieObyazId, FullCode("22270"), "Прочие начисленные расходы"),
                new(tekushieObyazId, FullCode("22300"), "Налоги к оплате"),
                new(tekushieObyazId, FullCode("22310"), "Налог на прибыль к оплате"),
                new(tekushieObyazId, FullCode("22320"), "НДС к оплате"),
                new(tekushieObyazId, FullCode("22330"), "Акцизы к оплате"),
                new(tekushieObyazId, FullCode("22340"), "Налог на имущество к оплате"),
                new(tekushieObyazId, FullCode("22350"), "Дорожный налог к оплате"),
                new(tekushieObyazId, FullCode("22360"), "Налог на землю к оплате"),
                new(tekushieObyazId, FullCode("22370"), "Прочие налоги к оплате"),
                new(tekushieObyazId, FullCode("22400"), "Резервы предстоящих расходов и платежей"),
                new(tekushieObyazId, FullCode("22410"), "Резервы на оплату отпускных"),
                new(tekushieObyazId, FullCode("22420"), "Резервы на гарантийное обслуживание"),
                new(tekushieObyazId, FullCode("22430"), "Резервы судебных исков"),
                new(tekushieObyazId, FullCode("22440"), "Прочие начисленные резервы"),
                new(tekushieObyazId, FullCode("22500"), "Прочие краткосрочные обязательства"),
                new(tekushieObyazId, FullCode("22510"), "Обязательства перед учредителями"),
                new(tekushieObyazId, FullCode("22520"), "Кредиторская задолженность дочерних (материнских) предприятий"),
                new(tekushieObyazId, FullCode("22530"), "Краткосрочные обязательства прекращенной деятельности"),
                
                // ========== ДОЛГОСРОЧНЫЕ ОБЯЗАТЕЛЬСТВА ==========
                new(dolgoObyazId, FullCode("22600"), "Долгосрочные обязательства"),
                new(dolgoObyazId, FullCode("22610"), "Облигации к оплате"),
                new(dolgoObyazId, FullCode("22620"), "Банковские кредиты, займы"),
                new(dolgoObyazId, FullCode("22630"), "Прочие кредиты, займы"),
                new(dolgoObyazId, FullCode("22640"), "Векселя к оплате"),
                new(dolgoObyazId, FullCode("22650"), "Обязательства по финансовой аренде"),
                new(dolgoObyazId, FullCode("22660"), "Долгосрочные обязательства прекращенной деятельности"),
                new(dolgoObyazId, FullCode("22670"), "Прочие долгосрочные обязательства"),
                new(dolgoObyazId, FullCode("22691"), "Дисконты (скидки) по долгосрочным облигациям"),
                new(dolgoObyazId, FullCode("22692"), "Премии (надбавки) по долгосрочным облигациям"),
                new(dolgoObyazId, FullCode("22700"), "Отсроченные доходы"),
                new(dolgoObyazId, FullCode("22710"), "Отсроченные доходы - гранты"),
                new(dolgoObyazId, FullCode("22720"), "Долгосрочные авансы, полученные"),
                new(dolgoObyazId, FullCode("22730"), "Прочие отсроченные доходы"),
                new(dolgoObyazId, FullCode("22800"), "Отсроченные налоговые обязательства"),

               // ========== СОБСТВЕННЫЙ КАПИТАЛ ==========
                new(kapitalId, FullCode("33000"), "Объявленный (уставный) капитал"),
                new(kapitalId, FullCode("33010"), "Простые акции"),
                new(kapitalId, FullCode("33020"), "Привилегированные акции"),
                new(kapitalId, FullCode("33030"), "Дополнительно оплаченный капитал"),
                new(kapitalId, FullCode("33090"), "Выкупленные собственные акции"),
                new(kapitalId, FullCode("33100"), "Добавочный капитал"),
                new(kapitalId, FullCode("33110"), "Гранты и целевые финансирования"),
                new(kapitalId, FullCode("33120"), "Корректировки по переоценке основных средств"),
                new(kapitalId, FullCode("33130"), "Корректировки по переоценке прочих активов"),
                new(kapitalId, FullCode("33140"), "Курсовые разницы по операциям с иностранным подразделениям"),
                new(kapitalId, FullCode("33150"), "Безвозмездно полученные ценности"),
                new(kapitalId, FullCode("33160"), "Прочий добавочный капитал"),
                new(kapitalId, FullCode("33200"), "Нераспределенная прибыль"),
                new(kapitalId, FullCode("33210"), "Нераспределенная прибыль отчетного года"),
                new(kapitalId, FullCode("33220"), "Нераспределенная прибыль прошлых лет"),
                new(kapitalId, FullCode("33300"), "Резервный капитал"),
                new(kapitalId, FullCode("33400"), "Доля меньшинства"),

                // ========== ОПЕРАЦИОННЫЕ ДОХОДЫ ==========
                new(operDohodiId, FullCode("44000"), "Доходы от операционной деятельности"),
                new(operDohodiId, FullCode("44010"), "Доходы от реализации"),
                new(operDohodiId, FullCode("44020"), "Прочие доходы от операционной деятельности"),
                new(operDohodiId, FullCode("44090"), "Возврат проданных товаров и скидки"),
                new(operDohodiId, FullCode("44100"), "Доходы от биологических активов"),
                new(operDohodiId, FullCode("44110"), "Прибыль (убыток) от первоначального признания биологических активов"),
                new(operDohodiId, FullCode("44120"), "Доход от сбора сельхозпродукции"),
                new(operDohodiId, FullCode("44130"), "Прибыль (убыток) от изменения справедливой стоимости биологических активов"),

                // ========== ОПЕРАЦИОННЫЕ РАСХОДЫ ==========
                new(operRashodiId, FullCode("55000"), "Себестоимость реализованных запасов, работ и услуг"),
                new(operRashodiId, FullCode("55010"), "Себестоимость реализации"),
                new(operRashodiId, FullCode("55020"), "Корректировка стоимости запасов"),
                new(operRashodiId, FullCode("55030"), "Расходы по транспортировке запасов"),
                new(operRashodiId, FullCode("55040"), "Расходы на переработку"),
                new(operRashodiId, FullCode("55050"), "Расходы на приобретение запасов"),
                new(operRashodiId, FullCode("55100"), "Расходы по производству биологических активов"),
                new(operRashodiId, FullCode("55200"), "Реализационные расходы"),
                new(operRashodiId, FullCode("55210"), "Расходы на рекламу и содействие продаж"),
                new(operRashodiId, FullCode("55220"), "Расходы на оплату труда"),
                new(operRashodiId, FullCode("55230"), "Расходы по отчислениям в социальные фонды"),
                new(operRashodiId, FullCode("55240"), "Расходы по хранению и транспортировке"),
                new(operRashodiId, FullCode("55250"), "Расходы по безнадежным долгам"),
                new(operRashodiId, FullCode("55260"), "Расходы по гарантийному обслуживанию"),
                new(operRashodiId, FullCode("55270"), "Расходы по износу и амортизации"),
                new(operRashodiId, FullCode("55280"), "Расходы на премиальные вознаграждения"),
                new(operRashodiId, FullCode("55290"), "Прочие реализационные расходы"),
                new(operRashodiId, FullCode("55300"), "Общие и административные расходы"),
                new(operRashodiId, FullCode("55310"), "Расходы на оплату труда"),
                new(operRashodiId, FullCode("55311"), "Расходы по отчислениям в социальные фонды"),
                new(operRashodiId, FullCode("55312"), "Расходы по аренде"),
                new(operRashodiId, FullCode("55313"), "Расходы по оплате профессиональных услуг"),
                new(operRashodiId, FullCode("55314"), "Налоги и сборы"),
                new(operRashodiId, FullCode("55315"), "Расходы офисных принадлежностей"),
                new(operRashodiId, FullCode("55316"), "Ремонт и техобслуживание основных средств"),
                new(operRashodiId, FullCode("55317"), "Расходы по компьютерному обеспечению"),
                new(operRashodiId, FullCode("55318"), "Командировочные расходы"),
                new(operRashodiId, FullCode("55319"), "Штрафы, пени, неустойки"),
                new(operRashodiId, FullCode("55320"), "Расходы на исследования и научные разработки"),
                new(operRashodiId, FullCode("55321"), "Расходы на износ основных средств"),
                new(operRashodiId, FullCode("55322"), "Расходы по амортизации нематериальных активов"),
                new(operRashodiId, FullCode("55323"), "Расходы по аудиту"),
                new(operRashodiId, FullCode("55324"), "Расходы по НДС, не принимаемому к зачету"),
                new(operRashodiId, FullCode("55325"), "Коммунальные расходы"),
                new(operRashodiId, FullCode("55326"), "Коммуникационные расходы"),
                new(operRashodiId, FullCode("55327"), "Расходы электроэнергии"),
                new(operRashodiId, FullCode("55328"), "Расходы топлива"),
                new(operRashodiId, FullCode("55329"), "Представительские расходы"),
                new(operRashodiId, FullCode("55330"), "Расходы на благотворительные цели"),
                new(operRashodiId, FullCode("55350"), "Прочие общие и административные расходы"),

                // ========== ДОХОДЫ И РАСХОДЫ ОТ НЕОПЕРАЦИОННОЙ ДЕЯТЕЛЬНОСТИ ==========
                new(neoperDohodiRashodiId, FullCode("66000"), "Доходы от неоперационной деятельности"),
                new(neoperDohodiRashodiId, FullCode("66010"), "Доходы в виде процентов"),
                new(neoperDohodiRashodiId, FullCode("66020"), "Доходы от инвестиций"),
                new(neoperDohodiRashodiId, FullCode("66030"), "Доходы от дивидендов"),
                new(neoperDohodiRashodiId, FullCode("66040"), "Доходы от курсовых разниц"),
                new(neoperDohodiRashodiId, FullCode("66050"), "Доходы от конвертации"),
                new(neoperDohodiRashodiId, FullCode("66060"), "Доходы от выбытия долгосрочных активов"),
                new(neoperDohodiRashodiId, FullCode("66070"), "Прочие неоперационные доходы"),
                new(neoperDohodiRashodiId, FullCode("66100"), "Расходы от неоперационной деятельности"),
                new(neoperDohodiRashodiId, FullCode("66110"), "Расходы в виде процентов"),
                new(neoperDohodiRashodiId, FullCode("66120"), "Убытки от инвестиции"),
                new(neoperDohodiRashodiId, FullCode("66130"), "Убытки от обесценения"),
                new(neoperDohodiRashodiId, FullCode("66140"), "Убытки от курсовых разниц"),
                new(neoperDohodiRashodiId, FullCode("66150"), "Убытки от конвертации"),
                new(neoperDohodiRashodiId, FullCode("66160"), "Убытки от выбытия долгосрочных активов"),
                new(neoperDohodiRashodiId, FullCode("66170"), "Прочие неоперационные расходы"),
                new(neoperDohodiRashodiId, FullCode("66200"), "Расходы (доходы) по налогу на прибыль"),
                new(neoperDohodiRashodiId, FullCode("70000"), "Свод доходов и расходов")
            };

            int accountsAdded = 0;

            // Грузим только существующие коды счетов, чтобы не было дублей при повторном запуске
            var existingAccountsCodes = await _context.IfrsAccounts.Select(a => a.AccountNumber).ToHashSetAsync(ct);

            foreach (var item in accountsToSeed)
            {
                // Наша логика типа счета (Активный/Пассивный) идеально работает по первой цифре
                AccountNature nature = DetermineAccountNature(item.Code);

                if (!existingAccountsCodes.Contains(item.Code))
                {
                    var newAccount = new IfrsAccount
                    {
                        AccountNumber = item.Code,
                        AccountName = item.Name,
                        CategoryAccountId = item.CategoryId, // Используем твой точный GUID
                        AccountNature = nature,
                        IsActive = true,
                        Description = "Импортировано автоматически",
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.IfrsAccounts.Add(newAccount);

                    await _context.SaveChangesAsync(ct);

                    existingAccountsCodes.Add(item.Code);
                    accountsAdded++;

                    await Task.Delay(1, ct);
                }
            }

            if (accountsAdded > 0)
            {
                await _context.SaveChangesAsync(ct);
                _logger.LogInformation("Успешно добавлено {Count} новых счетов", accountsAdded);
            }

            return accountsAdded;
        }

        // --- УМНОЕ ОПРЕДЕЛЕНИЕ ТИПА СЧЕТА ПО БУХГАЛТЕРСКИМ ПРАВИЛАМ ---
        private AccountNature DetermineAccountNature(string code)
        {
            if (string.IsNullOrEmpty(code))
                return AccountNature.ActivePassive;

            char firstDigit = code[0];
            string firstThreeDigits = code.Length >= 3 ? code.Substring(0, 3) : "";

            return firstDigit switch
            {
                '1' => AccountNature.Active,  // Активы
                '2' => AccountNature.Passive, // Обязательства
                '3' => AccountNature.Passive, // Капитал
                '4' => AccountNature.Passive, // Доходы
                '5' => AccountNature.Active,  // Расходы

                // Неоперационные
                '6' when firstThreeDigits == "660" => AccountNature.Passive, // Доходы
                '6' when firstThreeDigits == "661" || firstThreeDigits == "662" => AccountNature.Active, // Расходы

                '7' => AccountNature.ActivePassive, // Сводный счет

                _ => AccountNature.ActivePassive
            };
        }
    }
}
