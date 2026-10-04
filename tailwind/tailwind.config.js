// Tailwind สำหรับเว็บ ASP.NET (production-version)
// ใช้ prefix "tw-" และปิด preflight เพื่อไม่ให้ชนกับ Bootstrap ที่ DevExpress ใช้อยู่
// สีทั้งหมดอ้างตัวแปร CSS (--c-*) ใน input.css ซึ่งสลับค่าตามธีมมืด/สว่าง (class "dark" บน <html>)
//
// build:  ดับเบิลคลิก tailwind\build.cmd
const v = (name) => `rgb(var(--c-${name}) / <alpha-value>)`

module.exports = {
  prefix: 'tw-',
  darkMode: 'class',
  corePlugins: { preflight: false },
  // class ที่ประกอบขึ้นในโค้ด VB (Dash.Kpi, Dash.StatusBadge) สแกนไม่เจอ ต้องระบุไว้ตรงนี้
  safelist: [{ pattern: /^tw-(bg|text)-(accent|gold|info|ok|warn|bad)(\/15)?$/ }],
  content: {
    relative: true,
    files: [
      '../test/production-version/*.{aspx,master,vb}',
      '../test/production-version/assets/js/ui.js',
      '../test/App_Code/*.vb',
    ],
  },
  theme: {
    extend: {
      fontFamily: {
        sans: ['"Noto Sans Thai"', 'system-ui', 'sans-serif'],
      },
      colors: {
        bg: v('bg'),
        surface: v('surface'),
        'surface-2': v('surface-2'),
        line: v('line'),
        ink: v('ink'),
        muted: v('muted'),
        accent: v('accent'),
        gold: v('gold'),
        'accent-ink': v('accent-ink'),
        ok: v('ok'),
        warn: v('warn'),
        bad: v('bad'),
        info: v('info'),
      },
    },
  },
}
