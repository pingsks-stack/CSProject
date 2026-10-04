/* UI กลางของเว็บ: ธีม, เมนู, การแจ้งเตือน, Notiflix และตัวแปลง Swal.fire เดิม
   ต้องโหลดหลัง notiflix-aio (และ lucide ถ้ามี) */
(function () {
  var FONT = '"Noto Sans Thai", system-ui, sans-serif';
  // Notiflix ครอบชื่อฟอนต์ด้วยเครื่องหมายคำพูดเอง จึงต้องส่งชื่อเปล่า ๆ
  var NF_FONT = 'Noto Sans Thai';
  var root = document.documentElement;

  function store(key, value) {
    try { if (value === undefined) return localStorage.getItem(key); localStorage.setItem(key, value); } catch (e) { return null; }
  }
  function cssColor(name, alpha) {
    var v = getComputedStyle(root).getPropertyValue('--c-' + name).trim().split(/\s+/).join(',');
    return alpha == null ? 'rgb(' + v + ')' : 'rgba(' + v + ',' + alpha + ')';
  }

  // ===== Notiflix: สีตามธีมปัจจุบัน =====
  function initNotiflix() {
    if (!window.Notiflix) return;
    var overlay = 'rgba(0,0,0,0.55)';
    var surface = cssColor('surface'), ink = cssColor('ink');
    var common = { backgroundColor: surface, titleColor: ink, messageColor: cssColor('muted'), backOverlayColor: overlay };
    function t(color) { var o = { svgColor: color, buttonBackground: color, buttonColor: '#fff' }; for (var k in common) o[k] = common[k]; return o; }
    Notiflix.Report.init({
      fontFamily: NF_FONT, svgSize: '56px', borderRadius: '16px', backgroundColor: surface,
      titleFontSize: '19px', messageFontSize: '15px', buttonFontSize: '15px',
      success: t(cssColor('ok')), failure: t(cssColor('bad')), warning: t(cssColor('warn')), info: t(cssColor('accent')),
    });
    Notiflix.Notify.init({ fontFamily: NF_FONT, position: 'right-top', borderRadius: '12px', timeout: 3500 });
    Notiflix.Confirm.init({
      fontFamily: NF_FONT, borderRadius: '16px', backgroundColor: surface, titleColor: ink, messageColor: cssColor('muted'),
      okButtonBackground: cssColor('accent'), okButtonColor: cssColor('accent-ink'), backOverlayColor: overlay,
    });
    Notiflix.Loading.init({ fontFamily: NF_FONT, svgColor: cssColor('accent') });
  }
  initNotiflix();

  // ===== ตัวแปลง Swal.fire({ title, text|html, icon, confirmButtonText }).then(...) เดิมให้ใช้ Notiflix =====
  var ICON_TO_REPORT = { success: 'success', error: 'failure', warning: 'warning', info: 'info', question: 'info' };
  function fire(a, b, c) {
    var o = (a && typeof a === 'object') ? a : { title: a, text: b, icon: c };
    return new Promise(function (resolve) {
      var done = function () { resolve({ isConfirmed: true, value: true }); };
      if (!window.Notiflix) { alert((o.title || '') + '\n' + (o.text || '')); done(); return; }
      Notiflix.Report[ICON_TO_REPORT[o.icon] || 'info'](
        o.title || '', o.html || o.text || '', o.confirmButtonText || 'ตกลง', done, { plainText: !o.html });
    });
  }
  window.Swal = window.swal = { fire: fire };

  // ===== กราฟ (ApexCharts) — วาดใหม่อัตโนมัติเมื่อสลับธีม =====
  var charts = [];
  function base() {
    var dark = root.classList.contains('dark');
    return {
      chart: { fontFamily: FONT, foreColor: cssColor('muted'), toolbar: { show: false }, background: 'transparent', zoom: { enabled: false }, animations: { enabled: false } },
      grid: { borderColor: cssColor('line'), strokeDashArray: 4 },
      theme: { mode: dark ? 'dark' : 'light' },
      tooltip: { theme: dark ? 'dark' : 'light' },
      dataLabels: { enabled: false },
      legend: { labels: { colors: cssColor('muted') } },
      noData: { text: 'ยังไม่มีข้อมูล', style: { color: cssColor('muted'), fontFamily: FONT } },
    };
  }
  function merge(a, b) {
    for (var k in b) {
      if (b[k] && typeof b[k] === 'object' && !Array.isArray(b[k]) && a[k] && typeof a[k] === 'object') merge(a[k], b[k]);
      else a[k] = b[k];
    }
    return a;
  }
  function draw(id, build) {
    var el = document.getElementById(id);
    if (!el || !window.ApexCharts) return;
    var entry = { el: el, build: build, chart: null };
    entry.render = function () {
      if (entry.chart) entry.chart.destroy();
      entry.chart = new ApexCharts(el, merge(base(), build()));
      entry.chart.render();
    };
    charts.push(entry);
    entry.render();
  }
  document.addEventListener('themechange', function () { charts.forEach(function (c) { c.render(); }); });

  window.UI = {
    color: cssColor,
    // กราฟพื้นที่หลายชุด series = [{ name, data }]
    area: function (id, labels, series) {
      draw(id, function () {
        return {
          chart: { type: 'area', height: 300 },
          series: series, colors: [cssColor('accent'), cssColor('info'), cssColor('warn')],
          xaxis: { categories: labels, axisBorder: { show: false }, axisTicks: { show: false } },
          yaxis: { labels: { formatter: function (v) { return Math.round(v); } } },
          stroke: { curve: 'smooth', width: 2 },
          fill: { type: 'gradient', gradient: { opacityFrom: 0.45, opacityTo: 0.05 } },
          legend: { position: 'top', horizontalAlign: 'right' },
        };
      });
    },
    // แท่งแนวนอน
    hbar: function (id, labels, data, name) {
      draw(id, function () {
        return {
          chart: { type: 'bar', height: Math.max(220, labels.length * 34) },
          series: [{ name: name || 'จำนวน', data: data }],
          colors: [cssColor('accent')],
          plotOptions: { bar: { horizontal: true, borderRadius: 4, barHeight: '55%' } },
          xaxis: { categories: labels, labels: { formatter: function (v) { return Math.round(v); } } },
        };
      });
    },
    // โดนัท
    donut: function (id, labels, data) {
      draw(id, function () {
        return {
          chart: { type: 'donut', height: 280 },
          series: data, labels: labels,
          colors: [cssColor('ok'), cssColor('info'), cssColor('warn'), cssColor('bad'), cssColor('accent'), cssColor('muted')],
          stroke: { colors: [cssColor('surface')] },
          legend: { position: 'bottom' },
          plotOptions: { pie: { donut: { size: '68%', labels: { show: true, total: { show: true, label: 'ทั้งหมด', color: cssColor('muted') }, value: { color: cssColor('ink') } } } } },
        };
      });
    },
    // กราฟเส้นเล็กบนการ์ด
    spark: function (id, data, tone) {
      draw(id, function () {
        var c = cssColor(tone || 'accent');
        return {
          chart: { type: 'area', height: 40, sparkline: { enabled: true } },
          series: [{ data: data }], colors: [c],
          stroke: { curve: 'straight', width: 1.6 },
          fill: { type: 'gradient', gradient: { opacityFrom: 0.35, opacityTo: 0 } },
          tooltip: { enabled: false },
        };
      });
    },
  };

  document.addEventListener('DOMContentLoaded', function () {
    if (window.lucide) lucide.createIcons();

    // ไฮไลต์เมนูของหน้าปัจจุบัน
    var page = location.pathname.split('/').pop().toLowerCase();
    document.querySelectorAll('[data-nav] a[href]').forEach(function (a) {
      if (a.getAttribute('href').toLowerCase() === page) a.classList.add('is-active');
    });

    // พับ/ขยายหมวดเมนู (จำไว้ในเบราว์เซอร์)
    document.querySelectorAll('[data-nav-group]').forEach(function (g, i) {
      var items = g.querySelector('.nav-group-items');
      var key = 'navgroup-' + i;
      if (store(key) === 'closed') items.style.display = 'none';
      g.querySelector('[data-group-toggle]').addEventListener('click', function () {
        var closed = items.style.display === 'none';
        items.style.display = closed ? '' : 'none';
        store(key, closed ? 'open' : 'closed');
      });
    });

    // ย่อเมนูเหลือแต่ไอคอน
    var collapse = document.getElementById('collapse-btn');
    if (collapse) collapse.addEventListener('click', function () {
      var mini = root.classList.toggle('sidebar-mini');
      store('sidebar', mini ? 'mini' : 'full');
    });

    // เมนูบนมือถือ
    var sidebar = document.getElementById('app-sidebar');
    var backdrop = document.getElementById('app-backdrop');
    function toggle(open) {
      if (!sidebar) return;
      sidebar.classList.toggle('tw--translate-x-full', !open);
      if (backdrop) backdrop.classList.toggle('tw-hidden', !open);
    }
    document.querySelectorAll('[data-sidebar-open]').forEach(function (b) { b.addEventListener('click', function () { toggle(true); }); });
    if (backdrop) backdrop.addEventListener('click', function () { toggle(false); });

    // สลับธีม
    var themeBtn = document.getElementById('theme-toggle');
    if (themeBtn) themeBtn.addEventListener('click', function () {
      var dark = root.classList.toggle('dark');
      store('theme', dark ? 'dark' : 'light');
      initNotiflix();
      document.dispatchEvent(new CustomEvent('themechange'));
    });

    // กล่องการแจ้งเตือน
    var nb = document.getElementById('notif-btn'), np = document.getElementById('notif-panel');
    if (nb && np) {
      nb.addEventListener('click', function (e) { e.stopPropagation(); np.classList.toggle('tw-hidden'); });
      document.addEventListener('click', function (e) { if (!np.contains(e.target)) np.classList.add('tw-hidden'); });
    }

    // Ctrl+K ไปที่ช่องค้นหา
    var search = document.getElementById('global-search');
    if (search) document.addEventListener('keydown', function (e) {
      if ((e.ctrlKey || e.metaKey) && e.key.toLowerCase() === 'k') { e.preventDefault(); search.focus(); }
    });
  });
})();
