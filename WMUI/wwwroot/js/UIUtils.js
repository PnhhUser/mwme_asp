// =====================================================
// DOM Util Library (Bootstrap + jQuery) - Version 0.1
// Bootstrap version - 5.3.8
// jQuery version - 3.7.1
// =====================================================

const UIUtils = {
  // ---------------------------
  // SELECTOR
  // ---------------------------

  /** Lấy phần tử bằng selector */
  get: (s) => (s instanceof jQuery ? s : $(s)),

  /** Lấy phần tử theo ID */
  id: (s) => UIUtils.get(`#${s}`),

  /** Lấy phần tử theo Class */
  cls: (s) => UIUtils.get(`.${s}`),

  /** Lấy parent element */
  parent: (s) => UIUtils.get(s).parent(),

  /** Lấy toàn bộ children */
  children: (parent) => UIUtils.get(parent).children(),

  /** Lấy child theo index */
  childAt: (parent, index) => UIUtils.get(parent).children().eq(index),

  /** Lấy children theo selector */
  childrenOf: (parent, child) => UIUtils.get(parent).children(child),

  /**
   * Tìm tất cả các phần tử con bên trong parent theo selector, tìm sâu (all levels)
   */
  findChild: (parent, selector) => UIUtils.get(parent).find(selector),

  /** Lấy phần tử phía trước */
  prev: (s) => UIUtils.get(s).prev(),

  /** Lấy phần tử phía sau */
  next: (s) => UIUtils.get(s).next(),

  // ---------------------------
  // CLASS
  // ---------------------------
  class: {
    /** Thêm 1 class */
    add: (s, c) => UIUtils.get(s).addClass(c),

    /** Thêm nhiều class từ array */
    addAll: (s, arr) => UIUtils.get(s).addClass(arr.join(" ")),

    /** Xóa 1 class */
    remove: (s, c) => UIUtils.get(s).removeClass(c),

    /** Xóa nhiều class */
    removeAll: (s, arr) => UIUtils.get(s).removeClass(arr.join(" ")),

    /** Toggle class */
    toggle: (s, c) => UIUtils.get(s).toggleClass(c),

    /** Kiểm tra có class không */
    has: (s, c) => UIUtils.get(s).hasClass(c),

    /** Thay class cũ bằng class mới */
    replace: (s, oldC, newC) => UIUtils.get(s).removeClass(oldC).addClass(newC),
  },

  // ---------------------------
  // ATTRIBUTES
  // ---------------------------

  /** Gán 1 attribute hoặc lấy giá trị attribute */
  attr: (s, name, value) => UIUtils.get(s).attr(name, value),

  /** Gán nhiều attributes từ object */
  attrs: (s, obj) => {
    Object.entries(obj).forEach(([k, v]) => UIUtils.get(s).attr(k, v));
  },

  /** Lấy giá trị khi dùng data- */
  data: (s, name) => UIUtils.get(s).data(name),

  // ---------------------------
  // EVENTS
  // ---------------------------
  event: {
    /** Lắng nghe event */
    on: (s, ev, fn) => UIUtils.get(s).on(ev, fn),

    /** Delegate event từ parent */
    delegate: (p, s, ev, fn) => UIUtils.get(p).on(ev, s, fn),

    /** Trigger 1 event */
    trigger: (s, ev, data) => UIUtils.get(s).trigger(ev, data),
  },

  // ---------------------------
  // CONTENT
  // ---------------------------

  /** Get/set text */
  text: (s, v) =>
    v === undefined ? UIUtils.get(s).text() : UIUtils.get(s).text(v),

  /** Get/set HTML */
  html: (s, v) =>
    v === undefined ? UIUtils.get(s).html() : UIUtils.get(s).html(v),

  // ---------------------------
  // CSS
  // ---------------------------
  css: {
    /** Lấy 1 CSS property */
    get: (s, prop) => UIUtils.get(s).css(prop),

    /** Set 1 hoặc nhiều CSS property */
    set: (s, prop, value) => {
      if (typeof prop === "object") UIUtils.get(s).css(prop);
      else UIUtils.get(s).css(prop, value);
    },

    /**
     * Remove 1 hoặc nhiều CSS property
     *
     * Ví dụ:
     * UIUtils.css.remove("#myDiv", "color");          // remove color
     * UIUtils.css.remove("#myDiv", ["color","font-size"]); // remove nhiều property
     */
    remove: (s, props) => {
      const el = UIUtils.get(s);
      if (Array.isArray(props)) {
        props.forEach((p) => el.css(p, ""));
      } else {
        el.css(props, "");
      }
    },
  },

  // ---------------------------
  // ELEMENT CREATE / REMOVE
  // ---------------------------

  /** Append child vào parent */
  append: (p, child) => UIUtils.get(p).append(child),

  /** Xóa phần tử */
  remove: (s) => UIUtils.get(s).remove(),

  /**
   * Tạo element mới + set attributes
   *
   * Lưu ý:
   * - Nếu có key tên thuộc tính và value là nội dung bên trong
   *
   * Ví dụ:
   * const el = UIUtils.create("div", { id: "myDiv", class: "card" });
   */
  create: (tag, props = {}) => {
    const el = UIUtils.get(document.createElement(tag));
    UIUtils.attrs(el, props);
    return el;
  },

  // ---------------------------
  // FORM / INPUT
  // ---------------------------
  input: {
    // Format input thành VND khi nhập
    moneyFormat: function (id, type = "vi-VN") {
      UIUtils.event.on(id, "input", (e) => {
        let v = e.target.value.replace(/\D/g, "");
        e.target.value = v ? parseInt(v).toLocaleString(type) : "";
      });
    },

    // Lấy giá trị VND từ input (dạng số nguyên)
    getVNDValue: function (selector) {
      const value = $(selector).val(); // lấy giá trị input
      const number = value ? parseInt(value.replace(/\./g, "")) : 0;
      return number;
    },

    // Lấy tên file
    getFileName: function (files) {
      if (!files || files.length === 0) return null;
      return files[0].name.replace(/ /g, "_");
    },
  },
  form: {
    /** Enable/disable input */
    disable: (s, state) => UIUtils.get(s).prop("disabled", state),

    colors: {
      invalid: "rgba(255,0,0,0.1)",
      valid: "rgba(0,255,0,0.1)",
    },

    /** Các class chuẩn của Bootstrap validation */
    classes: {
      invalid: "is-invalid",
      valid: "is-valid",
      invalidFeedback: "invalid-feedback",
      validFeedback: "valid-feedback",
    },

    /** Set input invalid + nền đỏ nhạt */
    setInvalidWithBg: function (s, customColor) {
      UIUtils.class.replace(s, this.classes.valid, this.classes.invalid);
      UIUtils.css.set(
        s,
        "background-color",
        customColor || this.colors.invalid
      );
    },

    /** Set input valid + nền xanh nhạt */
    setValidWithBg: function (s, customColor) {
      UIUtils.class.replace(s, this.classes.invalid, this.classes.valid);
      UIUtils.css.set(s, "background-color", customColor || this.colors.valid);
    },

    /** Tạo / update span báo lỗi hoặc hợp lệ */
    spanValidate: function (s, msg, cls = this.classes.invalidFeedback) {
      const input = UIUtils.get(s);
      let span = input.next("span");

      if (span.length === 0) {
        span = UIUtils.create("span", { class: cls });
        input.after(span);
      } else {
        span
          .removeClass(
            `${this.classes.validFeedback} ${this.classes.invalidFeedback}`
          )
          .addClass(cls);
      }

      span.text(msg);
    },

    /**
     * Validate input field
     * - Tự động kiểm tra checkbox, radio, input, textarea, select
     * - Checkbox: nếu checked thì lấy value, không checked → coi là rỗng
     * - Radio: lấy radio được chọn theo name, nếu không chọn → coi là rỗng
     * - Text, textarea, select: trim() và kiểm tra rỗng
     *
     * Ví dụ:
     * UIUtils.form.validateField("#myInput", "Tên");
     * UIUtils.form.validateField("input[name='gender']", "Giới tính"); // radio
     * UIUtils.form.validateField("#agree", "Điều khoản"); // checkbox
     */
    validateField: function (selector, fieldName) {
      const $el = UIUtils.get(selector);
      let value = $el.val();

      if ($el.is(":checkbox")) {
        value = $el.is(":checked") ? value : "";
      }

      if ($el.is(":radio")) {
        const checkedRadio = UIUtils.get(
          `input[name='${$el.attr("name")}']:checked`
        );
        value = checkedRadio.val() || "";
      }

      const isValid =
        value !== undefined && value !== null && value.toString().trim() !== "";

      if (isValid) {
        this.setValidWithBg($el);
        this.spanValidate(
          $el,
          `${fieldName} hợp lệ`,
          this.classes.validFeedback
        );
      } else {
        this.setInvalidWithBg($el);
        this.spanValidate(
          $el,
          `${fieldName} không hợp lệ`,
          this.classes.invalidFeedback
        );
      }
    },

    /**
     * Check toàn bộ form → disable nút submit nếu có lỗi
     * - Kiểm tra các field có class is-invalid
     * - Kiểm tra các input/textarea/select required có rỗng hay không
     * - Nếu có field invalid hoặc required rỗng → disable submit button
     *
     * Ví dụ:
     * UIUtils.form.check("#myForm", "#btnSubmit");
     */
    check: function (formSel, submitBtnSel) {
      const form = UIUtils.get(formSel);

      const hasInvalid = form.find(`.${this.classes.invalid}`).length > 0;

      let emptyRequired = false;
      form
        .find("input[required], textarea[required], select[required]")
        .each(function () {
          const $el = UIUtils.get(this);
          if (!$el.val().trim()) emptyRequired = true;
        });

      UIUtils.form.disable(submitBtnSel, hasInvalid || emptyRequired);
    },

    /** Auto-validate input khi nhập / đổi / focus */
    inputValidate: function (selector, name) {
      const el = UIUtils.get(selector);
      const apply = (e) => this.validateField(e.target, name);

      if (el.is("input") || el.is("textarea"))
        UIUtils.event.on(selector, "keyup", apply);

      if (el.is("select")) UIUtils.event.on(selector, "change", apply);

      UIUtils.event.on(selector, "focus", apply);
    },
  },

  // ---------------------------
  // MODAL
  // ---------------------------
  modal: {
    classes: {
      show: "show.bs.modal",
      shown: "shown.bs.modal",
      hide: "hide.bs.modal",
      hidden: "hidden.bs.modal",
    },
    init: function (id, opts = {}) {
      const modalEl = UIUtils.id(id);
      if (modalEl.length === 0) return null;

      const defaultOptions = {
        focus: false,
        backdrop: "static", // có thể là true, false hoặc "static"
        keyboard: false,
      };

      const finalOptions = { ...defaultOptions, ...opts };

      // Lấy hoặc tạo instance Bootstrap Modal
      const instance = bootstrap.Modal.getOrCreateInstance(
        modalEl[0],
        finalOptions
      );

      return {
        el: modalEl,
        instance,
        classes: this.classes,

        // Hiển thị modal, có thể truyền trigger element
        show: (trigger = null) => instance.show(trigger),
        // Ẩn modal
        hide: () => instance.hide(),
        // Event Modal
        on: function (eventName, callback) {
          return UIUtils.event.on(modalEl, eventName, callback);
        },
      };
    },
  },

  // ---------------------------
  // TOAST
  // ---------------------------
  toast: {
    classes: {
      show: "show.bs.toast",
      shown: "shown.bs.toast",
      hide: "hide.bs.toast",
      hidden: "hidden.bs.toast",
      body: ".toast-body",
    },
    init: function (id, opts = {}) {
      const toastEl = UIUtils.id(id);
      if (toastEl.length === 0) return null;

      const defaultOptions = {
        delay: 2500,
        autohide: true,
      };

      const instance = bootstrap.Toast.getOrCreateInstance(toastEl, {
        ...defaultOptions,
        ...opts,
      });

      return {
        el: toastEl,
        instance,
        classes: this.classes,
        /**
         * content - jQuery object đại diện cho phần body của toast
         * Lưu ý: đã là jQuery object, không cần wrap thêm UIUtils.get()
         *
         * Ví dụ:
         * const toast = UIUtils.toast.init("myToast");
         * toast.content.text("Thông báo mới"); // set text
         */
        content: toastEl.find(this.classes.body),

        // Hiển thị toast
        show: () => instance.show(),
        // Ẩn modal
        hide: () => instance.hide(),
        // Event
        on: function (eventName, callback) {
          return UIUtils.event.on(toastEl, eventName, callback);
        },
        // Trả về giá trị boolean theo trạng thái hiển thị của toast.
        isShown: () => instance.isShown(),
      };
    },
  },
  // ---------------------------
  // AJAX UTILS
  // ---------------------------
  ajax: {
    /**
     * Gửi GET request
     * @param {string} url - URL API
     * @param {object} data - Dữ liệu query string {key: value}
     * @param {function} successCallback - Hàm callback khi request thành công, nhận response
     * @param {function} errorCallback - Hàm callback khi request lỗi
     * @param {string} dataType - Kiểu dữ liệu trả về từ server ('json', 'text', 'html'...), mặc định 'json'
     */
    get: function (
      url,
      data = {},
      successCallback,
      errorCallback,
      dataType = "json"
    ) {
      $.ajax({
        url: url,
        type: "GET",
        data: data, // sẽ convert object → query string ?key=value
        dataType: dataType, // định dạng dữ liệu trả về
        success: function (response) {
          successCallback && successCallback(response); // gọi callback thành công
        },
        error: function (xhr, status, error) {
          console.error("AJAX GET error:", status, error);
          errorCallback && errorCallback(xhr, status, error); // gọi callback lỗi
        },
      });
    },

    /**
     * Gửi POST request
     * url - URL API
     * data - Dữ liệu gửi đi (JS object hoặc FormData)
     * successCallback - Callback khi request thành công
     * errorCallback - Callback khi request lỗi
     * dataType - Kiểu dữ liệu trả về từ server, mặc định 'json'
     * contentType - Kiểu Content-Type gửi đi:
     *   - "application/json" → gửi JSON string
     *   - false → gửi FormData (browser tự set multipart/form-data)
     *   - undefined → mặc định application/x-www-form-urlencoded
     */
    post: function (
      url,
      data = {},
      successCallback,
      errorCallback,
      dataType = "json",
      contentType
    ) {
      let sendData = data;
      const isFormData = data instanceof FormData;

      if (contentType === "application/json") {
        sendData = JSON.stringify(data);
      }

      $.ajax({
        url: url,
        type: "POST",
        data: sendData,
        dataType: dataType,
        contentType: isFormData
          ? false
          : contentType || "application/x-www-form-urlencoded; charset=UTF-8",
        processData: isFormData ? false : true,
        success: function (response) {
          successCallback && successCallback(response);
        },
        error: function (xhr, status, error) {
          console.error("AJAX POST error:", status, error);
          errorCallback && errorCallback(xhr, status, error);
        },
      });
    },
  },

  // ---------------------------
  // Date
  // ---------------------------

  date: {
    /**
     * Lấy giờ hiện tại dạng hh:mm (24h)
     */
    nowHHMM: function () {
      return new Date().toLocaleTimeString("vi-VN", {
        hour: "2-digit",
        minute: "2-digit",
        hour12: false,
      });
    },

    /**
     * Lấy giờ hiện tại dạng hh:mm:ss (24h)
     */
    nowHHMMSS: function () {
      return new Date().toLocaleTimeString("vi-VN", {
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
        hour12: false,
      });
    },

    /**
     * Xác định buổi trong ngày dựa trên giờ hiện tại
     * Quy ước:
     *   00:00 - 11:59 -> "sáng"
     *   12:00 - 13:59 -> "trưa"
     *   14:00 - 16:59 -> "chiều"
     *   17:00 - 20:59 -> "tối"
     *   21:00 - 23:59 -> "khuya"
     */
    getPeriod: function () {
      const hour = new Date().getHours();

      const periods = [
        [0, 12, "sáng"],
        [12, 14, "trưa"],
        [14, 17, "chiều"],
        [17, 21, "tối"],
        [21, 24, "khuya"],
      ];

      for (const [start, end, label] of periods) {
        if (hour >= start && hour < end) return label;
      }
    },

    /**
     * Chạy đồng hồ real-time, gọi callback mỗi giây
     * hàm nhận giá trị thời gian (string hh:mm:ss)
     * intervalId dùng để clearInterval khi cần dừng
     * vd:
     * const id = UIUtils.date.startClock(time => console.log(time));
     * clearInterval(id); // dừng đồng hồ
     */
    startClock: function (cb) {
      // Gọi ngay 1 lần để hiển thị không bị delay
      cb(UIUtils.date.nowHHMMSS());

      // Chạy interval mỗi 1 giây
      const intervalId = setInterval(() => {
        cb(UIUtils.date.nowHHMMSS());
      }, 1000);

      return intervalId;
    },
  },
};
