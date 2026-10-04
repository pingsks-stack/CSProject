<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="Chat.aspx.vb" Inherits="production_version_Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-4 tw-flex tw-flex-wrap tw-items-end tw-justify-between tw-gap-3">
        <div>
            <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">แชท</h1>
            <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted"><%: Subtitle %></p>
        </div>
    </div>

    <div class="panel tw-grid tw-overflow-hidden md:tw-grid-cols-[320px_1fr]" style="height: calc(100vh - 13rem); min-height: 480px">
        <%-- รายการคู่สนทนา --%>
        <div id="chat-sidebar" class="tw-flex tw-min-h-0 tw-flex-col tw-border-b tw-border-line md:tw-border-b-0 md:tw-border-r">
            <div class="tw-space-y-2 tw-border-b tw-border-line tw-p-3">
                <label class="tw-m-0 tw-flex tw-h-9 tw-items-center tw-gap-2 tw-rounded-lg tw-bg-surface-2 tw-px-3 tw-text-muted">
                    <i data-lucide="search" class="tw-h-4 tw-w-4"></i>
                    <input id="conv-filter" type="search" placeholder="ค้นหารายชื่อ" class="tw-w-full tw-border-0 tw-bg-transparent tw-text-sm tw-text-ink tw-outline-none" style="background:transparent!important;border:0!important">
                </label>
                <select id="new-chat" class="form-control tw-h-9 tw-text-sm">
                    <option value="">+ เริ่มแชทใหม่กับ…</option>
                </select>
            </div>
            <div id="conv-list" class="tw-min-h-0 tw-flex-1 tw-overflow-y-auto"></div>
        </div>

        <%-- ห้องแชท --%>
        <div class="tw-flex tw-min-h-0 tw-flex-col">
            <div id="thread-head" class="tw-flex tw-h-16 tw-items-center tw-gap-3 tw-border-b tw-border-line tw-px-5">
                <div class="tw-text-sm tw-text-muted">เลือกคู่สนทนาทางซ้าย หรือเริ่มแชทใหม่</div>
            </div>
            <div id="thread" class="tw-min-h-0 tw-flex-1 tw-space-y-2 tw-overflow-y-auto tw-bg-bg/40 tw-px-5 tw-py-4"></div>
            <div class="tw-flex tw-items-end tw-gap-2 tw-border-t tw-border-line tw-p-3">
                <textarea id="composer" rows="1" maxlength="500" placeholder="พิมพ์ข้อความ… (Enter เพื่อส่ง, Shift+Enter ขึ้นบรรทัดใหม่)" disabled
                    class="form-control tw-max-h-32 tw-min-h-[42px] tw-flex-1 tw-resize-none tw-text-sm"></textarea>
                <button type="button" id="send-btn" class="btn-accent tw-h-[42px]" disabled><i data-lucide="send-horizontal"></i>ส่ง</button>
            </div>
        </div>
    </div>

    <script>
        (function () {
            var current = null, lastId = 0, contacts = [], convs = [];
            var listEl = document.getElementById('conv-list'), threadEl = document.getElementById('thread');
            var headEl = document.getElementById('thread-head'), composer = document.getElementById('composer'), sendBtn = document.getElementById('send-btn');

            function api(params, body) {
                var opts = { credentials: 'same-origin', headers: { 'X-Requested-With': 'fetch' } };
                if (body) { opts.method = 'POST'; opts.body = new URLSearchParams(body); }
                return fetch('ChatApi.ashx?' + new URLSearchParams(params), opts).then(function (r) {
                    return r.json().then(function (d) { if (!r.ok) throw new Error(d.message || 'error'); return d; });
                });
            }
            function el(tag, cls, text) { var e = document.createElement(tag); if (cls) e.className = cls; if (text != null) e.textContent = text; return e; }
            function initials(name) {
                var parts = (name || '?').split(' ').filter(function (p) { return p && ['นาย', 'นาง', 'นางสาว', 'อาจารย์', 'ดร.'].indexOf(p) < 0; });
                return ((parts[0] || name || '?').charAt(0) + (parts[1] ? parts[1].charAt(0) : '')).toUpperCase();
            }
            function avatar(name) { return el('span', 'tw-flex tw-h-9 tw-w-9 tw-shrink-0 tw-items-center tw-justify-center tw-rounded-full tw-bg-accent/20 tw-text-xs tw-font-bold tw-text-accent', initials(name)); }

            // ===== รายการคู่สนทนา =====
            function renderList() {
                var q = document.getElementById('conv-filter').value.trim().toLowerCase();
                listEl.innerHTML = '';
                var items = convs.filter(function (c) { return !q || c.name.toLowerCase().indexOf(q) >= 0; });
                if (!items.length) { listEl.appendChild(el('div', 'tw-px-4 tw-py-10 tw-text-center tw-text-sm tw-text-muted', 'ยังไม่มีบทสนทนา')); return; }
                items.forEach(function (c) {
                    var a = el('a', 'tw-flex tw-cursor-pointer tw-gap-3 tw-border-b tw-border-line tw-px-4 tw-py-3 tw-no-underline hover:tw-bg-surface-2 hover:tw-no-underline' + (current === c.email ? ' tw-bg-accent/10' : ''));
                    a.addEventListener('click', function () { open(c.email); });
                    a.appendChild(avatar(c.name));
                    var mid = el('div', 'tw-min-w-0 tw-flex-1');
                    mid.appendChild(el('div', 'tw-truncate tw-text-sm tw-font-semibold tw-text-ink', c.name));
                    mid.appendChild(el('div', 'tw-truncate tw-text-xs tw-text-muted', (c.mine ? 'คุณ: ' : '') + c.last));
                    a.appendChild(mid);
                    var right = el('div', 'tw-flex tw-shrink-0 tw-flex-col tw-items-end tw-gap-1');
                    right.appendChild(el('span', 'tw-text-[11px] tw-text-muted', c.at));
                    if (c.unread > 0) right.appendChild(el('span', 'tw-flex tw-h-5 tw-min-w-[20px] tw-items-center tw-justify-center tw-rounded-full tw-bg-gold tw-px-1.5 tw-text-[11px] tw-font-bold tw-text-black', String(c.unread)));
                    a.appendChild(right);
                    listEl.appendChild(a);
                });
            }
            function loadList() { return api({ action: 'list' }).then(function (d) { convs = d; renderList(); }).catch(function () { }); }

            // ===== ห้องแชท =====
            function bubble(m) {
                var row = el('div', 'tw-flex ' + (m.mine ? 'tw-justify-end' : 'tw-justify-start'));
                var box = el('div', 'tw-max-w-[75%]');
                var b = el('div', 'tw-whitespace-pre-wrap tw-break-words tw-rounded-2xl tw-px-4 tw-py-2 tw-text-sm ' +
                    (m.mine ? 'tw-rounded-br-md tw-bg-accent tw-text-accent-ink' : 'tw-rounded-bl-md tw-bg-surface-2 tw-text-ink'), m.text);
                box.appendChild(b);
                box.appendChild(el('div', 'tw-mt-0.5 tw-px-1 tw-text-[11px] tw-text-muted ' + (m.mine ? 'tw-text-right' : ''), m.at));
                row.appendChild(box);
                return row;
            }
            function loadThread(scroll) {
                if (!current) return Promise.resolve();
                var who = current;
                return api({ action: 'thread', with: who, after: lastId }).then(function (d) {
                    if (who !== current) return;
                    var nearBottom = threadEl.scrollHeight - threadEl.scrollTop - threadEl.clientHeight < 80;
                    d.messages.forEach(function (m) { threadEl.appendChild(bubble(m)); lastId = Math.max(lastId, m.id); });
                    if (lastId === 0 && !threadEl.children.length) threadEl.appendChild(el('div', 'tw-py-10 tw-text-center tw-text-sm tw-text-muted', 'ยังไม่มีข้อความ เริ่มทักทายได้เลย'));
                    if (d.messages.length && (scroll || nearBottom)) threadEl.scrollTop = threadEl.scrollHeight;
                    if (d.messages.length) loadList();
                });
            }
            function open(email) {
                current = email; lastId = 0; threadEl.innerHTML = '';
                var c = convs.filter(function (x) { return x.email === email; })[0] || contacts.filter(function (x) { return x.email === email; })[0];
                var name = c ? c.name : email;
                headEl.innerHTML = '';
                headEl.appendChild(avatar(name));
                var t = el('div', 'tw-leading-tight');
                t.appendChild(el('div', 'tw-text-sm tw-font-semibold tw-text-ink', name));
                t.appendChild(el('div', 'tw-text-xs tw-text-muted', email));
                headEl.appendChild(t);
                composer.disabled = sendBtn.disabled = false;
                composer.focus();
                history.replaceState(null, '', 'Chat.aspx?with=' + encodeURIComponent(email));
                renderList();
                loadThread(true).catch(function (e) { Notiflix.Notify.failure('เปิดแชทไม่ได้: ' + e.message); });
            }
            function send() {
                var text = composer.value.trim();
                if (!text || !current) return;
                sendBtn.disabled = true;
                api({ action: 'send' }, { with: current, text: text }).then(function () {
                    composer.value = ''; composer.style.height = '';
                    if (lastId === 0) threadEl.innerHTML = '';
                    return loadThread(true);
                }).catch(function (e) { Notiflix.Notify.failure(e.message); })
                  .then(function () { sendBtn.disabled = false; composer.focus(); });
            }

            sendBtn.addEventListener('click', send);
            composer.addEventListener('keydown', function (e) { if (e.key === 'Enter' && !e.shiftKey) { e.preventDefault(); send(); } });
            composer.addEventListener('input', function () { composer.style.height = 'auto'; composer.style.height = Math.min(composer.scrollHeight, 128) + 'px'; });
            document.getElementById('conv-filter').addEventListener('input', renderList);
            document.getElementById('conv-filter').addEventListener('keydown', function (e) { if (e.key === 'Enter') e.preventDefault(); });
            document.getElementById('new-chat').addEventListener('change', function () { if (this.value) { open(this.value); this.value = ''; } });

            api({ action: 'contacts' }).then(function (d) {
                contacts = d;
                var sel = document.getElementById('new-chat');
                d.forEach(function (c) { var o = el('option', null, c.name + (c.related ? ' ★' : '')); o.value = c.email; sel.appendChild(o); });
            });
            loadList().then(function () {
                var w = new URLSearchParams(location.search).get('with');
                if (w) open(w); else if (convs.length) open(convs[0].email);
            });
            setInterval(function () { loadThread(false).catch(function () { }); }, 4000);
            setInterval(loadList, 15000);
        })();
    </script>
</asp:Content>
