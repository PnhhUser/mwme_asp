const modalUI = async (modal, btn) => {
  const el = modal.el[0];
  const id = UIUtils.data(btn, "id");
  const user = await getUser(id);

  UIUtils.findChild(el, ".modal-title").text(UIUtils.data(btn, "title"));
  UIUtils.findChild(el, ".modal-body").html(
    `<p>Bạn có chắc chắn muốn xóa người dùng <strong>${user.name}</strong> không?</p>`
  );
  UIUtils.findChild(el, ".modal-footer").html(`
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
            Hủy
        </button>
        <button type="button" class="btn btn-danger" id="confirm-delete" data-id="${user.id}">
            Xác nhận
        </button>
  `);

  btnRemove(modal, UIUtils.id("confirm-delete"));
};

const btnRemove = async function (modal, btn) {
  UIUtils.event.on(btn, "click", async function (e) {
    const $currentTarget = UIUtils.get(e.currentTarget);
    const id = $currentTarget.data("id");
    let tr = UIUtils.get(`tbody>tr[data-id="${id}"]`);

    const data = await postRemove(id);

    if (data.success) {
      tr.remove();
      modal.hide();

      // show toast thành công
    } else {
      modal.hide();
    }
  });
};

const getUser = async function (id) {
  try {
    let data = await UIUtils.ajax.getAsync("/Accounts?handler=Account", {
      id,
    });

    return data;
  } catch (e) {
    console.log(e);
    return e;
  }
};

const postRemove = async function (id) {
  const token = UIUtils.get("input[name='__RequestVerificationToken']").val();
  try {
    let data = await UIUtils.ajax.postAsync({
      url: "/Accounts?handler=Delete",
      headers: {
        RequestVerificationToken: token,
      },
      data: { id },
    });

    return data;
  } catch (e) {
    console.log(e);
    return e;
  }
};

const modalRemove = (event) => {
  const btn = event.currentTarget;
  const modal = UIUtils.modal.init("modal");

  modalUI(modal, btn);
  modal.show();

  let isHiding = false;

  modal.on(modal.classes.hide, function (e) {
    if (isHiding) {
      isHiding = false;
      return;
    }

    e.preventDefault();

    isHiding = true;

    UIUtils.class.add(modal.el, "hiding");

    setTimeout(() => {
      UIUtils.class.remove(modal.el, "hiding");
      modal.hide();
    }, 400);
  });
};

UIUtils.cls("remove").each((_, btn) => {
  UIUtils.event.on(btn, "click", modalRemove);
});
